using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class AuditRepository(ApplicationDbContext context) : IAuditRepository
    {
        public async Task<Result<IEnumerable<AuditEntity>>> GetAll()
        {
            try
            {
                var audits = await _context.Audits
                    .AsNoTracking()
                    .Select(audit => new AuditEntity
                    {
                        IdAudit = audit.IdAudit,
                        ViewAction = audit.ViewAction,
                        Action = audit.Action,
                        Date = audit.Date,
                        ModifiedBy = audit.ModifiedBy,
                        OldValue = audit.OldValue,
                        PostValue = audit.PostValue
                    })
                    .ToListAsync();

                return Result<IEnumerable<AuditEntity>>.Success(audits);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AuditEntity>>.Failure($"Error al obtener los registros de auditoría: {ex.Message}");
            }
        }

        private readonly ApplicationDbContext _context = context;

        public Task<Result<AuditEntity>> Create(AuditEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
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
