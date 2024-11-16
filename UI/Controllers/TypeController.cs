using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController(ITypeService typeService) : ControllerBase
    {
        private readonly ITypeService _typeService = typeService;

        /// <summary>
        /// Obtiene una lista de todos los tipos.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<TypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _typeService.GetTypes();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene un tipo específico por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TypeDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _typeService.GetTypeById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Crea un nuevo tipo.
        /// </summary>
        /// <param name="typeRequest">Los datos del tipo a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] TypeRequestDto typeRequest)
        {
            var result = await _typeService.CreateType(typeRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza un tipo existente.
        /// </summary>
        /// <param name="id">El ID del tipo a actualizar.</param>
        /// <param name="typeRequest">Los nuevos datos del tipo.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] TypeRequestDto typeRequest)
        {
            var result = await _typeService.UpdateType(id, typeRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un tipo por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _typeService.DeleteType(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
