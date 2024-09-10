using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface ICityRepository : IRepository<CatCityEntity>
    {
        Task<IEnumerable<CatCityEntity>> GetCitiesByDepartmentIdAsync(int departmentId);
    }
}
