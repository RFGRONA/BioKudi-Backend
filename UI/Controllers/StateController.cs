using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Editor")]
    public class StateController(IStateService stateService) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _stateService.GetStates();
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
                var result = await _stateService.GetStateById(id);
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
        public async Task<IActionResult> Post([FromBody] StateRequestDto state)
        {
            try
            {
                var result = await _stateService.CreateState(state);
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
        public async Task<IActionResult> Put(int id, [FromBody] StateRequestDto state)
        {
            try
            {
                var result = await _stateService.UpdateState(id, state);
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
                var result = await _stateService.DeleteState(id);
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
