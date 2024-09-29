using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {
        public Task<ListEntity>? Create(ListEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ListEntity>? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
