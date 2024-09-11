using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        public Task Create(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTypeEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatTypeEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTypeEntity>> GetTypesByTableRelationAsync(string tableRelation)
        {
            throw new NotImplementedException();
        }

        public void Update(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
