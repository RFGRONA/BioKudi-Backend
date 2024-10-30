using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _personService = personService;

        [HttpGet]
        [ProducesResponseType(typeof(List<PersonListCrudDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _personService.GetUsers();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonListCrudDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personService.GetCrudPersonById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonCrudRequestDto person)
        {
            var result = await _personService.UpdateCrudUser(id, person);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.DeleteUser(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}
