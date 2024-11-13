using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController(IActivityService activityService) : ControllerBase
    {
        private readonly IActivityService _activityService = activityService;

        /// <summary>
        /// Obtiene la lista de actividades.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _activityService.GetActivities();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene una actividad específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la actividad a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _activityService.GetActivityById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Crea una nueva actividad.
        /// </summary>
        /// <param name="activity">Datos de la actividad a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ActivityRequestDto activity)
        {
            var result = await _activityService.CreateActivity(activity);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Actualiza los datos de una actividad.
        /// </summary>
        /// <param name="id">El ID de la actividad a actualizar.</param>
        /// <param name="activity">Datos actualizados de la actividad.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ActivityRequestDto activity)
        {
            var result = await _activityService.UpdateActivity(id, activity);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Elimina una actividad por su ID.
        /// </summary>
        /// <param name="id">El ID de la actividad a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activityService.DeleteActivity(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}
