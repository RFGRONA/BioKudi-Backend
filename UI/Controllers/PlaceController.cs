using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController(IPlaceService _placeService) : ControllerBase
    {
        private readonly IPlaceService _placeService = _placeService;

        [HttpGet]
        [Route("GetStartCarrousel")]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(List<StartCarrouselDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStartCarrousel()
        {
            var result = await _placeService.GetStartCarrousel();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        [Route("GetListActivities")]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(List<PlaceListActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListActivities()
        {
            var result = await _placeService.GetListActivities();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(PlaceDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var result = await _placeService.GetPlaceById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<PlaceListCrudDto>), StatusCodes.Status200OK)]
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
