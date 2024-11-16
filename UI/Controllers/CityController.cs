using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController(ICityService _cityService) : ControllerBase
    {
        private readonly ICityService _cityService = _cityService;

        /// <summary>
        /// Obtiene la lista de ciudades.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<CityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _cityService.GetCities();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene una ciudad específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la ciudad a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _cityService.GetCityById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Crea una nueva ciudad.
        /// </summary>
        /// <param name="city">Datos de la ciudad a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CityRequestDto city)
        {
            var result = await _cityService.CreateCity(city);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Actualiza los datos de una ciudad.
        /// </summary>
        /// <param name="id">El ID de la ciudad a actualizar.</param>
        /// <param name="city">Datos actualizados de la ciudad.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] CityRequestDto city)
        {
            var result = await _cityService.UpdateCity(id, city);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Elimina una ciudad por su ID.
        /// </summary>
        /// <param name="id">El ID de la ciudad a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cityService.DeleteCity(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}