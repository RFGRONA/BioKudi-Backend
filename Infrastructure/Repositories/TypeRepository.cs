using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        public Task<CatTypeEntity>? Create(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTypeEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatTypeEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatTypeEntity>?> GetTypesByTableRelationAsync(string tableRelation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CatTypeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
