using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        public Task<PictureEntity>? Create(PictureEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PictureEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PictureEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PictureEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
