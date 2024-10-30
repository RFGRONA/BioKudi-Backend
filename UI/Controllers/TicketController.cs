using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController(ITicketService ticketService) : ControllerBase
    {
        private readonly ITicketService _ticketService = ticketService;

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<TicketResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _ticketService.GetTickets();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _ticketService.GetTicketById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketCreateRequestDto ticketRequest)
        {
            var result = await _ticketService.CreateTicket(ticketRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Put(int id, [FromBody] TicketUpdateRequestDto ticketRequest)
        {
            var result = await _ticketService.ResponseTicket(id, ticketRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut("Scalp/{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Scalp(int id, [FromBody] ScalpTicketRequestDto scalpTicket)
        {
            var result = await _ticketService.ScalpTicket(id, scalpTicket);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ticketService.DeleteTicket(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}