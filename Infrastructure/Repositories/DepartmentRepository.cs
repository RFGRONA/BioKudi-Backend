using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public Task<CatDepartmentEntity>? Create(CatDepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(CatDepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatDepartmentEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatDepartmentEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatDepartmentEntity>?> GetDepartmentsWithCities()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CatDepartmentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
