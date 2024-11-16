using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        public Task<Result<PersonEntity>> GetAccountByEmail(string email);
        public Task<Result<bool>> UpdateUserPassword(PersonEntity user);
    }
}