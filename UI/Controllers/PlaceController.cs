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

        /// <summary>
        /// Obtiene los elementos del carrusel de inicio.
        /// </summary>
        [HttpGet]
        [Route("GetStartCarrousel")]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(List<StartCarrouselDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStartCarrousel()
        {
            var result = await _placeService.GetStartCarrousel();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene una lista de actividades.
        /// </summary>
        [HttpGet]
        [Route("GetListActivities")]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(List<PlaceListActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListActivities()
        {
            var result = await _placeService.GetListActivities();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Busca lugares en función de los criterios proporcionados.
        /// </summary>
        /// <param name="request">Criterios de búsqueda.</param>
        [HttpPost]
        [Route("/Place/Search")]
        [ProducesResponseType(typeof(List<PlaceListActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPlaces([FromBody] PlaceSearchRequestDto request)
        {
            var result = await _placeService.SearchPlaces(request);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene detalles de un lugar por su ID.
        /// </summary>
        /// <param name="id">ID del lugar.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(PlaceDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var result = await _placeService.GetPlaceById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene un lugar aleatorio.
        /// </summary>
        [HttpGet]
        [Route("/Place/Random")]
        [Authorize]
        [ProducesResponseType(typeof(MapPlaceIdResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRandomPlace()
        {
            var result = await _placeService.GetMapRandomPlace();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene una lista de lugares para operaciones CRUD.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<PlaceListCrudDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlaces()
        {
            var result = await _placeService.GetCrudPlaces();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Crea un nuevo lugar.
        /// </summary>
        /// <param name="place">Datos del lugar a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreatePlace([FromBody] PlaceRequestDto place)
        {
            var result = await _placeService.CreatePlace(place);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza un lugar existente.
        /// </summary>
        /// <param name="id">ID del lugar a actualizar.</param>
        /// <param name="place">Nuevos datos del lugar.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdatePlace(int id, [FromBody] PlaceRequestDto place)
        {
            var result = await _placeService.UpdatePlace(id, place);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un lugar por su ID.
        /// </summary>
        /// <param name="id">ID del lugar a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var result = await _placeService.DeletePlace(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}