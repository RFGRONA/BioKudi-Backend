using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IDepartmentRepository : IRepository<CatDepartmentEntity>
    {
        Task<IEnumerable<CatDepartmentEntity>> GetDepartmentsWithCities();
    }
}
