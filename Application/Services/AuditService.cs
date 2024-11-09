using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Serilog;

namespace Biokudi_Backend.Application.Services
{
    public class AuditService(AuditMapping auditMapping, IAuditRepository auditRepository, EmailUtility emailUtility) : IAuditService
    {
        private readonly AuditMapping _auditMapping = auditMapping;
        private readonly IAuditRepository _auditRepository = auditRepository;
        private readonly EmailUtility _emailUtility = emailUtility;

        public async Task<Result<AuditReportDto>> GetAuditsReport(string viewAction)
        {
            var result = await _auditRepository.GetAll();

            if (!result.IsSuccess)
            {
                return Result<AuditReportDto>.Failure(result.ErrorMessage);
            }

            var filteredAudits = result.Value
                .Where(a => a.ViewAction == viewAction)
                .ToList();

            var auditDtos = filteredAudits.Select(a => _auditMapping.EntityToDto(a)).ToList();

            int totalRecords = filteredAudits.Count;

            var actionCounts = new Dictionary<string, int>
            {
                { "INSERT", filteredAudits.Count(a => a.Action == "INSERT") },
                { "UPDATE", filteredAudits.Count(a => a.Action == "UPDATE") },
                { "DELETE", filteredAudits.Count(a => a.Action == "DELETE") }
            };

            var lastWeekDates = Enumerable.Range(0, 7)
                .Select(offset => DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-offset)))
                .OrderByDescending(date => date)
                .ToList();

            var weeklyActivityData = lastWeekDates
                .Select(date => new WeeklyActivity
                {
                    Date = date,
                    InsertCount = filteredAudits.Count(a => a.Action == "INSERT" && a.Date.HasValue && DateOnly.FromDateTime(a.Date.Value.Date) == date),
                    UpdateCount = filteredAudits.Count(a => a.Action == "UPDATE" && a.Date.HasValue && DateOnly.FromDateTime(a.Date.Value.Date) == date),
                    DeleteCount = filteredAudits.Count(a => a.Action == "DELETE" && a.Date.HasValue && DateOnly.FromDateTime(a.Date.Value.Date) == date)
                })
                .ToList();

            var auditReport = new AuditReportDto
            {
                TotalRecords = totalRecords,
                ActionCounts = actionCounts,
                WeeklyActivityData = weeklyActivityData,
                AuditRecords = auditDtos
            };

            return Result<AuditReportDto>.Success(auditReport);
        }

        public async Task<Result<List<AuditDto>>> GetAllAudits()
        {
            var result = await _auditRepository.GetAll();

            if (!result.IsSuccess)
            {
                return Result<List<AuditDto>>.Failure(result.ErrorMessage);
            }

            var auditDtos = result.Value.Select(a => _auditMapping.EntityToDto(a)).ToList();

            return Result<List<AuditDto>>.Success(auditDtos);
        }

        public Result SendReportByEmail(SendReportEmailDto sendReportEmailDto)
        {
            try
            {
                string fileName = $"Reporte_{DateUtility.DateNowColombia():yyyyMMdd_HHmmss}.pdf";

                string emailMessage = _emailUtility.CreateReportEmail(sendReportEmailDto.TableName);

                _emailUtility.SendEmailWithAttachment(sendReportEmailDto.RecipientEmail, $"Reporte {sendReportEmailDto.TableName}", emailMessage, sendReportEmailDto.FileBase64, fileName);

                return Result.Success();
            }
            catch (Exception ex)
            {
                Log.Error("ERROR SENDING EMAIL WITH ATTACHMENT: ", ex);
                return Result.Failure("Error al enviar el correo con el reporte adjunto.");
            }
        }

    }
}