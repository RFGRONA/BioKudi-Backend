using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StateController(IStateService stateService) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;

        /// <summary>
        /// Obtiene una lista de todos los estados.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<StateDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _stateService.GetStates();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene un estado específico por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(StateDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _stateService.GetStateById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Crea un nuevo estado.
        /// </summary>
        /// <param name="state">Los datos del estado a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] StateRequestDto state)
        {
            var result = await _stateService.CreateState(state);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza un estado existente.
        /// </summary>
        /// <param name="id">El ID del estado a actualizar.</param>
        /// <param name="state">Los nuevos datos del estado.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] StateRequestDto state)
        {
            var result = await _stateService.UpdateState(id, state);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un estado por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stateService.DeleteState(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
