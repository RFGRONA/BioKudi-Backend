using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewEntity>
    {
        Task<IEnumerable<ReviewEntity>?> GetReviewsByPlaceIdAsync(int placeId);
    }
}
