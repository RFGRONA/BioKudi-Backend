using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<Result<IEnumerable<PersonEntity>>> GetAccountsDeleted();
        Task<Result<PersonEntity>> GetAccountByEmail(string email);
        Task<Result<IEnumerable<PersonEntity>>> GetAccountsByRole(int role);
        Task<Result<IEnumerable<PersonEntity>>> GetAccountsByState(int state);
    }
}
