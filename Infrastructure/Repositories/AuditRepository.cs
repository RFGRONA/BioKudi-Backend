using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        public Task Create(AuditEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AuditEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByAction(string action)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByActionAndDateRangeAsync(string action, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<AuditEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByModifiedBy(string modifiedBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByViewAction(string viewAction)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditEntity>> GetByViewActionAndAction(string viewAction, string action)
        {
            throw new NotImplementedException();
        }

        public void Update(AuditEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
