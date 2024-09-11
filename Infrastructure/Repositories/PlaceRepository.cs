using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        public Task Create(PlaceEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PlaceEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PlaceEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>> GetPlacesByActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>> GetPlacesByCity(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>> GetPlacesByDateCreated(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceEntity>> GetPlacesByState(int stateId)
        {
            throw new NotImplementedException();
        }

        public void Update(PlaceEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
