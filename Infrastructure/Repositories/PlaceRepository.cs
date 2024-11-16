using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PlaceRepository(ICacheService cacheService, ApplicationDbContext context) : IPlaceRepository
    {
        private const string CACHE_KEY = "PlaceCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<PlaceEntity>> Create(PlaceEntity entity)
        {
            if (_context == null)
                return Result<PlaceEntity>.Failure("Error al iniciar el contexto con la base de datos");

            try
            {
                var result = await _context.Places.Where(p => p.NamePlace == entity.NamePlace).FirstOrDefaultAsync();
                if (result != null)
                    return Result<PlaceEntity>.Failure("El lugar ya se encuentra registrado");

                var place = new Place
                {
                    NamePlace = entity.NamePlace,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    Address = entity.Address,
                    Description = entity.Description,
                    Link = entity.Link,
                    CityId = entity.CityId,
                    StateId = entity.StateId,
                    DateCreated = DateUtility.DateNowColombia(),
                    DateModified = DateUtility.DateNowColombia()
                };

                if (entity.Activities != null && entity.Activities.Any())
                {
                    var existingActivities = await _context.CatActivities
                        .Where(a => entity.Activities.Select(ea => ea.IdActivity).Contains(a.IdActivity))
                        .ToListAsync();
                    place.Activities = existingActivities;
                }

                _context.Places.Add(place);
                var success = await _context.SaveChangesAsync();
                if (success == 0)
                    return Result<PlaceEntity>.Failure("Error al guardar los datos en la base de datos");

                if (entity.Pictures != null && entity.Pictures.Any())
                {
                    foreach (var pictureEntity in entity.Pictures)
                    {
                        var picture = new Picture
                        {
                            Name = pictureEntity.Name,
                            Link = pictureEntity.Link,
                            DateCreated = DateTime.Now,
                            TypeId = 8,
                            PlaceId = place.IdPlace
                        };
                        _context.Pictures.Add(picture);
                    }
                    success = await _context.SaveChangesAsync();
                    if (success == 0)
                        return Result<PlaceEntity>.Failure("Error al guardar la imagen en la base de datos");
                }

                entity.IdPlace = place.IdPlace;
                entity.DateCreated = place.DateCreated;
                entity.DateModified = place.DateModified;
                _cacheService.Remove(CACHE_KEY);

                return Result<PlaceEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<PlaceEntity>.Failure($"Error al registrar el lugar: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            if (_context == null)
                return Result<bool>.Failure("Error al iniciar el contexto con la base de datos");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var place = await _context.Places
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .Include(p => p.Reviews)
                    .FirstOrDefaultAsync(p => p.IdPlace == id);

                if (place == null)
                    return Result<bool>.Failure("Lugar no encontrado");

                if (place.Activities != null && place.Activities.Any())
                {
                    foreach (var activity in place.Activities)
                    {
                        activity.Places.Remove(place);
                    }
                }

                if (place.Pictures != null && place.Pictures.Any())
                {
                    _context.Pictures.RemoveRange(place.Pictures);
                }

                if (place.Reviews != null && place.Reviews.Any())
                {
                    _context.Reviews.RemoveRange(place.Reviews);
                }

                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _cacheService.Remove(CACHE_KEY);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<bool>.Failure($"Error al eliminar el lugar: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<PlaceEntity>>> GetAll()
        {
            if (_context == null)
                return Result<IEnumerable<PlaceEntity>>.Failure("Error al iniciar el contexto con la base de datos");

            try
            {
                var cachedPlaces = _cacheService.GetCollection<PlaceEntity>(CACHE_KEY);
                if (cachedPlaces != null)
                    return Result<IEnumerable<PlaceEntity>>.Success(cachedPlaces);

                var places = await _context.Places
                    .AsNoTracking()
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .Include(p => p.Reviews)
                    .Include(p => p.City)
                    .Include(p => p.State)
                    .ToListAsync();

                var placeEntities = places.Select(result => new PlaceEntity
                {
                    IdPlace = result.IdPlace,
                    NamePlace = result.NamePlace,
                    Latitude = result.Latitude,
                    Longitude = result.Longitude,
                    Address = result.Address,
                    Description = result.Description,
                    Link = result.Link,
                    DateCreated = result.DateCreated,
                    DateModified = result.DateModified,
                    City = result.City != null ? new CatCityEntity
                    {
                        IdCity = result.City.IdCity,
                        NameCity = result.City.NameCity
                    } : null,
                    State = result.State != null ? new CatStateEntity
                    {
                        IdState = result.State.IdState,
                        NameState = result.State.NameState
                    } : null,
                    Activities = result.Activities?.Select(a => new CatActivityEntity
                    {
                        IdActivity = a.IdActivity,
                        NameActivity = a.NameActivity,
                        UrlIcon = a.UrlIcon
                    }).ToList() ?? new List<CatActivityEntity>(),
                    Pictures = result.Pictures?.Select(pic => new PictureEntity
                    {
                        IdPicture = pic.IdPicture,
                        Name = pic.Name,
                        Link = pic.Link
                    }).ToList() ?? new List<PictureEntity>(),
                    Rating = result.Reviews?.Any() == true ? result.Reviews.Average(r => (double)r.Rate) : 0
                }).OrderBy(p => p.NamePlace).ToList();

                _cacheService.SetCollection(CACHE_KEY, placeEntities, TimeSpan.FromHours(1));
                return Result<IEnumerable<PlaceEntity>>.Success(placeEntities);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PlaceEntity>>.Failure($"Error al obtener lugares principales: {ex.Message}");
            }
        }

        public async Task<Result<PlaceEntity>> GetById(int id)
        {
            if (_context == null)
                return Result<PlaceEntity>.Failure("Error al iniciar el contexto con la base de datos");

            try
            {
                var result = await _context.Places
                    .AsNoTracking()
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .Include(p => p.Reviews)
                    .ThenInclude(r => r.Person)
                    .Include(p => p.City)
                    .Include(p => p.State)
                    .FirstOrDefaultAsync(p => p.IdPlace == id);

                if (result == null)
                    return Result<PlaceEntity>.Failure("Lugar no encontrado");

                var place = new PlaceEntity
                {
                    IdPlace = result.IdPlace,
                    NamePlace = result.NamePlace,
                    Latitude = result.Latitude,
                    Longitude = result.Longitude,
                    Address = result.Address,
                    Description = result.Description,
                    Link = result.Link,
                    DateCreated = result.DateCreated,
                    DateModified = result.DateModified,
                    City = result.City != null ? new CatCityEntity
                    {
                        IdCity = result.City.IdCity,
                        NameCity = result.City.NameCity
                    } : null,
                    State = result.State != null ? new CatStateEntity
                    {
                        IdState = result.State.IdState,
                        NameState = result.State.NameState
                    } : null,
                    Activities = result.Activities?.Select(a => new CatActivityEntity
                    {
                        IdActivity = a.IdActivity,
                        NameActivity = a.NameActivity,
                        UrlIcon = a.UrlIcon
                    }).ToList() ?? new List<CatActivityEntity>(),
                    Pictures = result.Pictures?.Select(pic => new PictureEntity
                    {
                        IdPicture = pic.IdPicture,
                        Name = pic.Name,
                        Link = pic.Link
                    }).ToList() ?? new List<PictureEntity>(),
                    Reviews = result.Reviews?
                    .OrderByDescending(r => r.DateCreated)
                    .Take(5)
                    .Select(r => new ReviewEntity
                    {
                        IdReview = r.IdReview,
                        Comment = r.Comment,
                        Rate = r.Rate,
                        DateCreated = (DateTime)r.DateCreated,
                        DateModified = r.DateModified,
                        Person= r.Person,
                        PersonId = r.PersonId
                    }).ToList() ?? new List<ReviewEntity>(),
                    Rating = result.Reviews?.Any() == true ? result.Reviews.Average(r => (double)r.Rate) : 0
                };

                return Result<PlaceEntity>.Success(place);
            }
            catch (Exception ex)
            {
                return Result<PlaceEntity>.Failure($"Error al obtener el lugar: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(PlaceEntity place)
        {
            if (_context == null)
                return Result<bool>.Failure("Error al iniciar el contexto con la base de datos");

            try
            {
                var result = await _context.Places
                    .Where(p => p.IdPlace == place.IdPlace)
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .FirstOrDefaultAsync();

                if (result == null)
                    return Result<bool>.Failure("Lugar no encontrado");

                result.NamePlace = place.NamePlace;
                result.Latitude = place.Latitude;
                result.Longitude = place.Longitude;
                result.Address = place.Address;
                result.Description = place.Description;
                result.Link = place.Link;
                result.CityId = place.CityId;
                result.StateId = place.StateId;
                result.DateModified = DateUtility.DateNowColombia();

                var activityIds = place.Activities.Select(a => a.IdActivity).ToList();
                result.Activities = await _context.CatActivities
                    .Where(a => activityIds.Contains(a.IdActivity))
                    .ToListAsync();

                if (place.Pictures != null && place.Pictures.Any())
                {
                    var existingPictures = await _context.Pictures.Where(p => p.PlaceId == place.IdPlace).ToListAsync();
                    if (existingPictures.Any())
                    {
                        _context.Pictures.RemoveRange(existingPictures);
                    }

                    foreach (var pictureEntity in place.Pictures)
                    {
                        var picture = new Picture
                        {
                            Name = pictureEntity.Name,
                            Link = pictureEntity.Link,
                            DateCreated = DateTime.Now,
                            TypeId = 8,
                            PlaceId = result.IdPlace
                        };
                        _context.Pictures.Add(picture);
                    }
                }

                await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el lugar: {ex.Message}");
            }
        }
        public async Task<Result<IEnumerable<PlaceEntity>>> SearchPlaces(string place)
        {
            if (_context == null)
                return Result<IEnumerable<PlaceEntity>>.Failure("Error al iniciar el contexto con la base de datos");

            try
            {
                var allCachedPlaces = _cacheService.GetCollection<PlaceEntity>(CACHE_KEY);
                IEnumerable<PlaceEntity> filteredPlaces;

                if (allCachedPlaces != null)
                {
                    string lowerCasePlace = place.ToLower();
                    filteredPlaces = [.. allCachedPlaces
                .Where(p => p.NamePlace.ToLower().Contains(lowerCasePlace))
                .OrderBy(p => !p.NamePlace.ToLower().StartsWith(lowerCasePlace))
                .ThenBy(p => p.NamePlace)];
                }
                else
                {
                    string lowerCasePlace = place.ToLower();
                    var places = await _context.Places
                        .AsNoTracking()
                        .Include(p => p.Activities)
                        .Include(p => p.Pictures)
                        .Include(p => p.Reviews)
                        .Include(p => p.City)
                        .Include(p => p.State)
                        .Where(p => p.NamePlace.ToLower().Contains(lowerCasePlace))
                        .ToListAsync();

                    filteredPlaces = [.. places.Select(result => new PlaceEntity
            {
                IdPlace = result.IdPlace,
                NamePlace = result.NamePlace,
                Latitude = result.Latitude,
                Longitude = result.Longitude,
                Address = result.Address,
                Description = result.Description,
                Link = result.Link,
                DateCreated = result.DateCreated,
                DateModified = result.DateModified,
                City = result.City != null ? new CatCityEntity
                {
                    IdCity = result.City.IdCity,
                    NameCity = result.City.NameCity ?? string.Empty
                } : null,
                State = result.State != null ? new CatStateEntity
                {
                    IdState = result.State.IdState,
                    NameState = result.State.NameState
                } : null,
                Activities = result.Activities?.Select(a => new CatActivityEntity
                {
                    IdActivity = a.IdActivity,
                    NameActivity = a.NameActivity,
                    UrlIcon = a.UrlIcon ?? string.Empty
                }).ToList() ?? [],
                Pictures = result.Pictures?.Select(pic => new PictureEntity
                {
                    IdPicture = pic.IdPicture,
                    Name = pic.Name,
                    Link = pic.Link
                }).ToList() ?? [],
                Rating = result.Reviews?.Any() == true ? result.Reviews.Average(r => (double)r.Rate) : 0
            })
            .OrderBy(p => !p.NamePlace.ToLower().StartsWith(lowerCasePlace))
            .ThenBy(p => p.NamePlace)];
                }

                return Result<IEnumerable<PlaceEntity>>.Success(filteredPlaces);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PlaceEntity>>.Failure($"Error al buscar lugares: {ex.Message}");
            }
        }
    }
}