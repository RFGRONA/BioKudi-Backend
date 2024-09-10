using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface ITypeRepository : IRepository<CatTypeEntity>
    {
        Task<IEnumerable<CatTypeEntity>> GetTypesByTableRelationAsync(string tableRelation);
    }
}
