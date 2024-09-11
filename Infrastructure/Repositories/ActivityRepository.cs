using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        public Task Create(CatActivityEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatActivityEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatActivityEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatActivityEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CatActivityEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
 