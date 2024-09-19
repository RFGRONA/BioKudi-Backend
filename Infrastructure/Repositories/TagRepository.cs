using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task<CatTagEntity>? Create(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTagEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatTagEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTagEntity>?> GetTagsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
