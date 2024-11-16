using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class AuditMapping
    {
        public AuditDto EntityToDto(AuditEntity audit)
        {
            return new AuditDto
            {
                IdAudit = audit.IdAudit,
                ViewAction = audit.ViewAction,
                Action = audit.Action,
                Date = audit.Date,
                OldValue = audit.OldValue,
                PostValue = audit.PostValue
            };
        }
    }
}
