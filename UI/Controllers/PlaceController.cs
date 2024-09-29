using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController(IPlaceService _placeService) : ControllerBase
    {
        private readonly IPlaceService _placeService = _placeService;

        [HttpGet]
        [Route("GetStartCarrousel")]
        public async Task<IActionResult> GetStartCarrousel()
        {
            try
            {
                var result = await _placeService.GetStartCarrousel();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetPlaceById/{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            try
            { 
                var result = await _placeService.GetPlaceById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetCrudPlaces")]
        public async Task<IActionResult> GetPlaces()
        {
            try
            {
                var result = await _placeService.GetCrudPlaces();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePlace")]
        public async Task<IActionResult> CreatePlace([FromBody] PlaceRequestDto place)
        {
            try
            {
                var result = await _placeService.CreatePlace(place);
                if (result == true)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePlace/{id}")]
        public async Task<IActionResult> UpdatePlace(int id, [FromBody] PlaceRequestDto place)
        {
            try
            {
                var result = await _placeService.UpdatePlace(id, place);
                if (result == null)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePlace/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            try
            {
                var result = await _placeService.DeletePlace(id);
                if (result == true)
                {
                    return Ok();
                }
                return BadRequest();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
