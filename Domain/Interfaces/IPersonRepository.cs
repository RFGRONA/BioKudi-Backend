using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<IEnumerable<PersonEntity>?> GetAccountsDeleted();
        Task<IEnumerable<PersonEntity>?> GetAccountsByRole(int role);
        Task<IEnumerable<PersonEntity>?> GetAccountsByState(int role);
    }
}
