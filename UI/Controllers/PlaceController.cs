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
            var result = await _placeService.GetStartCarrousel();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

    }
}
