using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController(IActivityService activityService) : ControllerBase
    {
        private readonly IActivityService _activityService = activityService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _activityService.GetActivities();
                if (result == null || !result.Any())
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _activityService.GetActivityById(id);
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
        public async Task<IActionResult> Post([FromBody] ActivityRequestDto activity)
        {
            try
            {
                var result = await _activityService.CreateActivity(activity);
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
        public async Task<IActionResult> Put(int id, [FromBody] ActivityRequestDto activity)
        {
            try
            {
                var result = await _activityService.UpdateActivity(id, activity);
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
                var result = await _activityService.DeleteActivity(id);
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
