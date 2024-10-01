using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PlaceRepository(ICacheService cacheService, ApplicationDbContext context) : IPlaceRepository
    {
        private const string CACHE_KEY = "PlaceCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<PlaceEntity>? Create(PlaceEntity entity)
        {
            if (_context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            try
            {
                var result = await _context.Places.Where(p => p.NamePlace == entity.NamePlace).FirstOrDefaultAsync();
                if (result != null)
                    throw new InvalidOperationException("El lugar ya se encuentra registrado");

                var place = new Place
                {
                    NamePlace = entity.NamePlace,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    Address = entity.Address,
                    Description = entity.Description,
                    Link = entity.Link,
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
                var succes = await _context.SaveChangesAsync();
                if (succes == 0)
                    throw new InvalidOperationException("Error al guardar los datos en la base de datos");
                entity.IdPlace = place.IdPlace;
                entity.DateCreated = place.DateCreated;
                entity.DateModified = place.DateModified;
                _cacheService.Remove(CACHE_KEY);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar el lugar");
            }
        }

        public Task<bool> Delete(int id)
        {
            try
            {
                _context.Places.Remove(new Place { IdPlace = id });
                _context.SaveChanges();
                _cacheService.Remove(CACHE_KEY);
                return Task.FromResult(true);
            }
            catch
            {
                throw new Exception($"Error al eliminar el lugar");
            }
        }

        public async Task<IEnumerable<PlaceEntity>?> GetAll()
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<PlaceEntity>(CACHE_KEY);
                if (cachedPlaces != null)
                    return cachedPlaces;

                var places = await _context.Places
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .Include(p => p.Reviews)
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
                        NameActivity = a.NameActivity
                    }).ToList() ?? new List<CatActivityEntity>(),
                    Pictures = result.Pictures?.Select(pic => new PictureEntity
                    {
                        IdPicture = pic.IdPicture,
                        Name = pic.Name,
                        Link = pic.Link
                    }).ToList() ?? new List<PictureEntity>(),
                    Rating = result.Reviews?.Any() == true ? result.Reviews.Average(r => (double)r.Rate) : 0
                })
                .OrderBy(p => p.NamePlace)
                .ToList();


                _cacheService.SetCollection(CACHE_KEY, placeEntities, TimeSpan.FromHours(1));
                Console.WriteLine(placeEntities);
                return placeEntities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener lugares principales");
            }
        }

        public async Task<PlaceEntity>? GetById(int id)
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<PlaceEntity>(CACHE_KEY);
                var cachedPlace = cachedPlaces?.FirstOrDefault(p => p.IdPlace == id);
                if (cachedPlace != null)
                    return cachedPlace;
                var result = await _context.Places
                    .Include(p => p.Activities)
                    .Include(p => p.Pictures)
                    .Include(p => p.Reviews)
                    .Include(p => p.State)
                    .FirstOrDefaultAsync(p => p.IdPlace == id)
                    ?? throw new KeyNotFoundException("Lugar no encontrado");
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
                        NameActivity = a.NameActivity
                    }).ToList() ?? new List<CatActivityEntity>(),
                    Pictures = result.Pictures?.Select(pic => new PictureEntity
                    {
                        IdPicture = pic.IdPicture,
                        Name = pic.Name,
                        Link = pic.Link
                    }).ToList() ?? new List<PictureEntity>(),
                    Rating = result.Reviews?.Any() == true ? result.Reviews.Average(r => (double)r.Rate) : 0
                };
                return place;
            }
            catch
            {
                throw new Exception("Error al obtener el lugar");
            }
        }

        public async Task<bool> Update(PlaceEntity place)
        {
            try
            {
                var result = await _context.Places.Where(p => p.IdPlace == place.IdPlace)
                    .Include(p => p.Activities)
                    .FirstOrDefaultAsync();
                if (result == null)
                    throw new KeyNotFoundException("Lugar no encontrado");
                result.NamePlace = place.NamePlace;
                result.Latitude = place.Latitude;
                result.Longitude = place.Longitude;
                result.Address = place.Address;
                result.Description = place.Description;
                result.Link = place.Link;
                result.StateId = place.StateId;
                result.DateModified = DateUtility.DateNowColombia();
                var activityIds = place.Activities.Select(a => a.IdActivity).ToList();

                result.Activities = await _context.CatActivities
                    .Where(a => activityIds.Contains(a.IdActivity))
                    .ToListAsync();
                await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return true;
            }
            catch
            {
                throw new Exception("Error al actualizar el lugar");
            }
        }

        public Task<IEnumerable<PlaceEntity>?> GetPlacesByActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>?> GetPlacesByCity(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>?> GetPlacesByDateCreated(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>?> GetPlacesByState(int stateId)
        {
            throw new NotImplementedException();
        }
    }
}
