using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task Create(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTagEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatTagEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTagEntity>> GetTagsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public void Update(CatTagEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
