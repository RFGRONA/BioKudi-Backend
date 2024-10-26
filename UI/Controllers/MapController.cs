using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MapController(IPlaceService placeService, IReviewService reviewService) : ControllerBase
    {
        private readonly IPlaceService _placeService = placeService;
        private readonly IReviewService _reviewService = reviewService;

        [HttpGet]
        [Route("Points")]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(List<PlaceListPointMapDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Points()
        {
            var result = await _placeService.GetListPointMap();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("Place/{id}")]
        [OutputCache(Duration = 60)]
        [ProducesResponseType(typeof(PlaceMapDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMapPlaceById(int id)
        {
            var result = await _placeService.GetMapPlaceById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("Reviews/{id}")]
        [OutputCache(Duration = 60)]
        [ProducesResponseType(typeof(List<ReviewMapResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsPlaceById(int id)
        {
            var result = await _reviewService.GetReviewsByPlaceId(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
