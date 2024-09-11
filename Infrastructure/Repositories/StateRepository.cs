using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class StateRepository : IStateRepository
    {
        public Task Create(CatStateEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatStateEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatStateEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatStateEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatStateEntity>> GetStatesByTableRelation(string tableRelation)
        {
            throw new NotImplementedException();
        }

        public void Update(CatStateEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
