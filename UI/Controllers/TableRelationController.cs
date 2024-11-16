using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Editor")]
    public class TableRelationController(ITableRelationService tableRelationService) : ControllerBase
    {
        private readonly ITableRelationService _tableRelationService = tableRelationService;

        /// <summary>
        /// Obtiene todas las relaciones de tablas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(TableRelationDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _tableRelationService.GetRelations();
            return Ok(result);
        }
    }
}
