using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController(ICityService _cityService) : ControllerBase
    {
        private readonly ICityService _cityService = _cityService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _cityService.GetCities();
                if (result == null)
                    return NotFound();
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _cityService.GetCityById(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CityRequestDto city)
        {
            try
            {
                var result = await _cityService.CreateCity(city);
                if (!result)
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CityRequestDto city)
        {
            try
            {
                var result = await _cityService.UpdateCity(id, city);
                if (!result)
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _cityService.DeleteCity(id);
                if (!result)
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
