using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class TicketMapping
    {
        public TicketEntity CreateRequestToEntity(TicketCreateRequestDto requestDto)
        {
            return new TicketEntity
            {
                Affair = requestDto.Affair,
                PersonId = requestDto.PersonId
            };
        }

        public TicketEntity UpdateRequestToEntity(TicketUpdateRequestDto requestDto, int ticketId)
        {
            return new TicketEntity
            {
                IdTicket = ticketId,
                AnsweredBy = requestDto.AnsweredBy,
                StateId = requestDto.StateId
            };
        }

        public TicketEntity ScalpRequestToEntity(ScalpTicketRequestDto requestDto, int ticketId)
        {
            return new TicketEntity
            {
                IdTicket = ticketId,
                ScalpAdmin = requestDto.ScalpAdmin
            };
        }

        public TicketResponseDto EntityToDto(TicketEntity entity)
        {
            return new TicketResponseDto
            {
                IdTicket = entity.IdTicket,
                Affair = entity.Affair,
                DateCreated = entity.DateCreated,
                DateAnswered = entity.DateAnswered,
                AnsweredBy = entity.AnsweredBy,
                ScalpAdmin = entity.ScalpAdmin,
                PersonId = entity.PersonId,
                PersonName = entity.Person?.NameUser, 
                StateId = entity.StateId,
                TypeId = entity.TypeId
            };
        }
    }

}
