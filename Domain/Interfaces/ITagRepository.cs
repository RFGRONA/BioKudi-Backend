using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface ITagRepository : IRepository<CatTagEntity>
    {
        Task<IEnumerable<CatTagEntity>> GetTagsByPostIdAsync(int postId);
    }
}
