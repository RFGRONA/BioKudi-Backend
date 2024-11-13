using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _personService = personService;

        /// <summary>
        /// Obtiene la lista de personas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonListCrudDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _personService.GetUsers();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene una persona específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la persona a obtener.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonListCrudDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personService.GetCrudPersonById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Actualiza los datos de una persona.
        /// </summary>
        /// <param name="id">El ID de la persona a actualizar.</param>
        /// <param name="person">Datos actualizados de la persona.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonCrudRequestDto person)
        {
            var result = await _personService.UpdateCrudUser(id, person);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Elimina una persona por su ID.
        /// </summary>
        /// <param name="id">El ID de la persona a eliminar.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.DeleteUser(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}