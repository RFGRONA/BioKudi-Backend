using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {
        public Task<Result<ListEntity>> Create(ListEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ListEntity>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<ListEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(ListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
