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

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Get()
        {
            var result = await _cityService.GetCities();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _cityService.GetCityById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CityRequestDto city)
        {
            var result = await _cityService.CreateCity(city);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] CityRequestDto city)
        {
            var result = await _cityService.UpdateCity(id, city);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

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
