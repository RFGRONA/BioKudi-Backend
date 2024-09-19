using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IStateRepository : IRepository<CatStateEntity>
    {
        Task<IEnumerable<CatStateEntity>?> GetStatesByTableRelation(string tableRelation);
    }
}
