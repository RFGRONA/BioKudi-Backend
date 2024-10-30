using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewEntity>
    {
        Task<Result<IEnumerable<ReviewEntity>>> GetReviewsByPlaceId(int placeId);
    }
}
