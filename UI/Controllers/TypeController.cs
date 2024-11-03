using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController(ITypeService typeService) : ControllerBase
    {
        private readonly ITypeService _typeService = typeService;

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<TypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _typeService.GetTypes();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TypeDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _typeService.GetTypeById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] TypeRequestDto typeRequest)
        {
            var result = await _typeService.CreateType(typeRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] TypeRequestDto typeRequest)
        {
            var result = await _typeService.UpdateType(id, typeRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _typeService.DeleteType(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}