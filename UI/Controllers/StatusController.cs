using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Biokudi_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        [HttpGet]
        [ProducesResponseType(typeof(StatusDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatus()
        {
            string status = "Healthy";
            bool dbConnection = false;

            try
            {
                dbConnection = await _dbContext.Database.CanConnectAsync();
            }
            catch
            {
                status = "Degraded";
            }

            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
            var uptime = DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime();

            var statusInfo = new StatusDto
            {
                Status = status,
                Version = version,
                Uptime = uptime,
                DatabaseConnection = dbConnection,
                Timestamp = DateTime.UtcNow
            };

            return Ok(statusInfo);
        }
    }
}