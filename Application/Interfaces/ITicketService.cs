using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Result<List<TicketResponseDto>>> GetTickets();
        Task<Result<TicketResponseDto>> GetTicketById(int id);
        Task<Result<bool>> CreateTicket(TicketCreateRequestDto ticket);
        Task<Result<bool>> ResponseTicket(int id, TicketUpdateRequestDto ticket);
        Task<Result<bool>> ScalpTicket(int id, ScalpTicketRequestDto ticket);
        Task<Result<bool>> DeleteTicket(int id); 
    }
}