using Biokudi_Backend.Application.Interfaces;
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

        public Task<PlaceEntity>? Create(PlaceEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PlaceEntity entity)
        {
            throw new NotImplementedException();
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

                var placeEntities = places.Select(place => new PlaceEntity
                {
                    IdPlace = place.IdPlace,
                    NamePlace = place.NamePlace,
                    Latitude = place.Latitude,
                    Longitude = place.Longitude,
                    Address = place.Address,
                    Description = place.Description,
                    Link = place.Link,
                    State = new CatStateEntity
                    {
                        IdState = place.State.IdState,
                        NameState = place.State.NameState
                    },
                    Activities = place.Activities.Select(a => new CatActivityEntity
                    {
                        IdActivity = a.IdActivity,
                        NameActivity = a.NameActivity
                    }).ToList(),
                    Pictures = place.Pictures.Select(pic => new PictureEntity
                    {
                        IdPicture = pic.IdPicture,
                        Name = pic.Name,
                        Link = pic.Link
                    }).ToList(),
                    Rating = place.Reviews.Any() ? place.Reviews.Average(r => (double)r.Rate) : 0
                })
                .OrderBy(p => p.NamePlace)
                .ToList();

                _cacheService.SetCollection(CACHE_KEY, placeEntities, TimeSpan.FromHours(1));

                return placeEntities;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<PlaceEntity>? GetById(int id)
        {
            throw new NotImplementedException();
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

        public Task<bool> Update(PlaceEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
