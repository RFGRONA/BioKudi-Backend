using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task<PostEntity>? Create(PostEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PostEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostEntity>?> GetPostsByPlaceIdAsync(int placeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PostEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
