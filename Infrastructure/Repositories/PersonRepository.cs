using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task<PersonEntity>? Create(PersonEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PersonEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsByRole(int role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsByState(int role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsDeleted()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PersonEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PersonEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
