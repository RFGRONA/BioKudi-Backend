using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPostRepository : IRepository<PostEntity>

    {
        //TODO: Obtener posts por tag
        Task<IEnumerable<PostEntity>> GetPostsByPlaceIdAsync(int placeId);
    }
}
