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

        /// <summary>
        /// Obtiene una lista de todos los tickets.
        /// </summary>
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

        /// <summary>
        /// Obtiene un ticket específico por su ID.
        /// </summary>
        /// <param name="id">El ID del ticket a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _ticketService.GetTicketById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Crea un nuevo ticket.
        /// </summary>
        /// <param name="ticketRequest">Los datos del ticket a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketCreateRequestDto ticketRequest)
        {
            var result = await _ticketService.CreateTicket(ticketRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza un ticket existente.
        /// </summary>
        /// <param name="id">El ID del ticket a actualizar.</param>
        /// <param name="ticketRequest">Los nuevos datos del ticket.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Put(int id, [FromBody] TicketUpdateRequestDto ticketRequest)
        {
            var result = await _ticketService.ResponseTicket(id, ticketRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Escala un ticket a otro nivel.
        /// </summary>
        /// <param name="id">El ID del ticket a escalar.</param>
        /// <param name="scalpTicket">Información para escalar el ticket.</param>
        [HttpPut("Scalp/{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Scalp(int id, [FromBody] ScalpTicketRequestDto scalpTicket)
        {
            var result = await _ticketService.ScalpTicket(id, scalpTicket);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un ticket por su ID.
        /// </summary>
        /// <param name="id">El ID del ticket a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ticketService.DeleteTicket(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
