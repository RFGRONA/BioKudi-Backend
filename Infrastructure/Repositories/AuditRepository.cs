using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        public Task<Result<AuditEntity>> Create(AuditEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<AuditEntity>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByAction(string action)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByActionAndDateRangeAsync(string action, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AuditEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByModifiedBy(string modifiedBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByViewAction(string viewAction)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>?> GetByViewActionAndAction(string viewAction, string action)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(AuditEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
