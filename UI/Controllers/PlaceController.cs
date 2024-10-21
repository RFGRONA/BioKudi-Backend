using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            var result = await _placeService.GetStartCarrousel();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        [Route("GetListActivities")]
        public async Task<IActionResult> GetListActivities()
        {
            var result = await _placeService.GetListActivities();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        [Route("GetListPointMap")]
        public async Task<IActionResult> GetListPointMap()
        {
            var result = await _placeService.GetListPointMap();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var result = await _placeService.GetPlaceById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetPlaces()
        {
            var result = await _placeService.GetCrudPlaces();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreatePlace([FromBody] PlaceRequestDto place)
        {
            var result = await _placeService.CreatePlace(place);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdatePlace(int id, [FromBody] PlaceRequestDto place)
        {
            var result = await _placeService.UpdatePlace(id, place);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var result = await _placeService.DeletePlace(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
