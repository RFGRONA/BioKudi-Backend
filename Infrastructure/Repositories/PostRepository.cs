using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task<Result<PostEntity>> Create(PostEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<PostEntity>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<PostEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostEntity>?> GetPostsByPlaceIdAsync(int placeId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(PostEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
