using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task<Result<CatTagEntity>> Create(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<CatTagEntity>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CatTagEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTagEntity>?> GetTagsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
