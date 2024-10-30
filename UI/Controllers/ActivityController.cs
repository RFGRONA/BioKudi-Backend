using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController(IActivityService activityService) : ControllerBase
    {
        private readonly IActivityService _activityService = activityService;

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _activityService.GetActivities();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _activityService.GetActivityById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ActivityRequestDto activity)
        {
            var result = await _activityService.CreateActivity(activity);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ActivityRequestDto activity)
        {
            var result = await _activityService.UpdateActivity(id, activity);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activityService.DeleteActivity(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}
