using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<CatRoleEntity>? Create(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatRoleEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatRoleEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
