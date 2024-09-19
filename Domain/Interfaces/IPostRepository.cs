using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPostRepository : IRepository<PostEntity>

    {
        Task<IEnumerable<PostEntity>?> GetPostsByPlaceIdAsync(int placeId);
    }
}
