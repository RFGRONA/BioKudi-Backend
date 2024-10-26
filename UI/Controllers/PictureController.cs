using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class PictureController(IPictureService pictureService) : ControllerBase
    {
        private readonly IPictureService _pictureService = pictureService;

        [HttpGet]
        [ProducesResponseType(typeof(List<PictureResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _pictureService.GetPictures();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pictureService.DeletePicture(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
