using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class TicketService(TicketMapping ticketMapping, ITicketRepository ticketRepository, EmailUtility emailUtility) : ITicketService
    {
        private readonly TicketMapping _ticketMapping = ticketMapping;
        private readonly ITicketRepository _ticketRepository = ticketRepository;
        private readonly EmailUtility _emailUtility = emailUtility;

        public async Task<Result<bool>> CreateTicket(TicketCreateRequestDto requestDto)
        {
            var entity = _ticketMapping.CreateRequestToEntity(requestDto);
            var result = await _ticketRepository.Create(entity);
            _emailUtility.SendEmail(requestDto.Email, $"Creación de ticket #{result.Value.IdTicket}", _emailUtility.TicketEmail(result.Value.IdTicket));
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteTicket(int id)
        {
            var result = await _ticketRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<TicketResponseDto>> GetTicketById(int id)
        {
            var result = await _ticketRepository.GetById(id);
            return result.IsSuccess
                ? Result<TicketResponseDto>.Success(_ticketMapping.EntityToDto(result.Value))
                : Result<TicketResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<TicketResponseDto>>> GetTickets()
        {
            var result = await _ticketRepository.GetAll();
            return result.IsSuccess
                ? Result<List<TicketResponseDto>>.Success(result.Value.Select(ticket => _ticketMapping.EntityToDto(ticket)).ToList())
                : Result<List<TicketResponseDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> ResponseTicket(int id, TicketUpdateRequestDto requestDto)
        {
            var entity = _ticketMapping.UpdateRequestToEntity(requestDto, id);
            entity.StateId = 7;
            var result = await _ticketRepository.Update(entity);
            _emailUtility.SendEmail(requestDto.Email, $"Respuesta ticket #{id}", _emailUtility.AnswerEmail(requestDto.AnsweredBy, requestDto.Response, id));
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> ScalpTicket(int id, ScalpTicketRequestDto scalpTicket)
        {
            var entity = _ticketMapping.ScalpRequestToEntity(scalpTicket, id);
            entity.StateId = 6;
            var result = await _ticketRepository.Update(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }
    }
}