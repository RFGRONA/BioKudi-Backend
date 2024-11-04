using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin, Editor")]
    public class ReportController(IAuditService auditService) : ControllerBase
    {
        private readonly IAuditService _auditService = auditService;

        [HttpGet("Audit")]
        [ProducesResponseType(typeof(List<AuditDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAuditRecords()
        {
            var result = await _auditService.GetAllAudits();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Activity")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivityReport()
        {
            var result = await _auditService.GetAuditsReport("cat_activity");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("City")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityReport()
        {
            var result = await _auditService.GetAuditsReport("cat_city");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Department")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartmentReport()
        {
            var result = await _auditService.GetAuditsReport("cat_department");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Person")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonReport()
        {
            var result = await _auditService.GetAuditsReport("person");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Picture")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPictureReport()
        {
            var result = await _auditService.GetAuditsReport("picture");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Place")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlaceReport()
        {
            var result = await _auditService.GetAuditsReport("place");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Review")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewReport()
        {
            var result = await _auditService.GetAuditsReport("review");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Role")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoleReport()
        {
            var result = await _auditService.GetAuditsReport("cat_role");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("State")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStateReport()
        {
            var result = await _auditService.GetAuditsReport("cat_state");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Ticket")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketReport()
        {
            var result = await _auditService.GetAuditsReport("ticket");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("Type")]
        [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTypeReport()
        {
            var result = await _auditService.GetAuditsReport("cat_type");
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpPost("SendByEmail")]
        public IActionResult SendReportByEmail([FromBody] SendReportEmailDto sendReportEmailDto)
        {
            if (sendReportEmailDto is not { RecipientEmail.Length: > 0, FileBase64.Length: > 0, TableName.Length: > 0 })
                return BadRequest(MessagesHelper.DataInvalidError);

            var result = _auditService.SendReportByEmail(sendReportEmailDto);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
