using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IAuditService
    {
        Task<Result<AuditReportDto>> GetAuditsReport(string viewAction);
        Task<Result<List<AuditDto>>> GetAllAudits();
        Result SendReportByEmail(SendReportEmailDto sendReportEmailDto);
    }
}
