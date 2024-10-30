using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StateController(IStateService stateService) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<StateDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _stateService.GetStates();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(StateDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _stateService.GetStateById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] StateRequestDto state)
        {
            var result = await _stateService.CreateState(state);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] StateRequestDto state)
        {
            var result = await _stateService.UpdateState(id, state);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stateService.DeleteState(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}