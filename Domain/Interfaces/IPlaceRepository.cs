using Biokudi_Backend.Domain.Entities;


namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPlaceRepository : IRepository<PlaceEntity>
    {
        Task<IEnumerable<PlaceEntity>?> GetPlacesByCity(int cityId);
        Task<IEnumerable<PlaceEntity>?> GetPlacesByActivity(int activityId);
        Task<IEnumerable<PlaceEntity>?> GetPlacesByState(int stateId);
        Task<IEnumerable<PlaceEntity>?> GetPlacesByDateCreated(DateTime date);
    }
}
