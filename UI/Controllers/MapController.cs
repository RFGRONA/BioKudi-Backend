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

        /// <summary>
        /// Obtiene una lista de puntos en el mapa.
        /// </summary>
        [HttpGet]
        [Route("Points")]
        [ProducesResponseType(typeof(List<PlaceListPointMapDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Points()
        {
            var result = await _placeService.GetListPointMap();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene los detalles de un lugar en el mapa por su ID.
        /// </summary>
        /// <param name="id">ID del lugar en el mapa.</param>
        [HttpGet("Place/{id}")]
        [ProducesResponseType(typeof(PlaceMapDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMapPlaceById(int id)
        {
            var result = await _placeService.GetMapPlaceById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene las reseñas de un lugar en el mapa por su ID.
        /// </summary>
        /// <param name="id">ID del lugar para obtener reseñas.</param>
        [HttpGet("Reviews/{id}")]
        [ProducesResponseType(typeof(List<ReviewMapResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsPlaceById(int id)
        {
            var result = await _reviewService.GetReviewsByPlaceId(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}