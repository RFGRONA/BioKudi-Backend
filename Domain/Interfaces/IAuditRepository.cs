using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IAuditRepository : IRepository<AuditEntity>
    {
        Task<IEnumerable<AuditEntity>?> GetByAction(string action);
        Task<IEnumerable<AuditEntity>?> GetByViewAction(string viewAction);
        Task<IEnumerable<AuditEntity>?> GetByViewActionAndAction(string viewAction, string action);
        Task<IEnumerable<AuditEntity>?> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AuditEntity>?> GetByModifiedBy(string modifiedBy);
        Task<IEnumerable<AuditEntity>?> GetByActionAndDateRangeAsync(string action, DateTime startDate, DateTime endDate);
    }
}
