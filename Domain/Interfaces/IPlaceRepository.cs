using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.ValueObject;


namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPlaceRepository : IRepository<PlaceEntity>
    {
        public Task<Result<IEnumerable<PlaceEntity>>> SearchPlaces(string place);
    }
}
