using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task Create(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatRoleEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatRoleEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CatRoleEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
