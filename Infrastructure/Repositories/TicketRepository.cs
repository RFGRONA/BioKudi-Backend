using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TicketRepository(ApplicationDbContext context) : ITicketRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<TicketEntity>> Create(TicketEntity entity)
        {
            try
            {
                var ticket = new Ticket
                {
                    Affair = entity.Affair,
                    DateCreated = DateUtility.DateNowColombia(),
                    PersonId = entity.PersonId,
                    StateId = 6,
                    TypeId = entity.TypeId
                };

                await _context.Tickets.AddAsync(ticket);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<TicketEntity>.Failure("No se pudo crear el ticket");

                entity.IdTicket = ticket.IdTicket;
                return Result<TicketEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<TicketEntity>.Failure($"Error al crear el ticket: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    return Result<bool>.Failure("El ticket no fue encontrado.");

                _context.Tickets.Remove(ticket);
                int rowsAffected = await _context.SaveChangesAsync();

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el ticket: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TicketEntity>>> GetAll()
        {
            try
            {
                var tickets = await _context.Tickets
                    .AsNoTracking()
                    .Select(ticket => new TicketEntity
                    {
                        IdTicket = ticket.IdTicket,
                        Affair = ticket.Affair,
                        DateCreated = ticket.DateCreated,
                        DateAnswered = ticket.DateAnswered ?? null,
                        AnsweredBy = ticket.AnsweredBy ?? null,
                        ScalpAdmin = ticket.ScalpAdmin ?? false,
                        PersonId = ticket.PersonId,
                        StateId = ticket.StateId ?? null,
                        TypeId = ticket.TypeId ?? null,
                        Person = new PersonEntity
                        {
                            NameUser = ticket.Person.NameUser,
                            Email = ticket.Person.Email
                        },
                        State = ticket.State != null ? new CatStateEntity
                        {
                            NameState = ticket.State.NameState,
                        } : null,
                        Type = ticket.Type != null ? new CatTypeEntity
                        {
                            NameType = ticket.Type.NameType ?? string.Empty,
                        } : null
                    })
                    .ToListAsync();

                return Result<IEnumerable<TicketEntity>>.Success(tickets);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TicketEntity>>.Failure($"Error al obtener los tickets: {ex.Message}");
            }
        }

        public async Task<Result<TicketEntity>> GetById(int id)
        {
            try
            {
                var ticket = await _context.Tickets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.IdTicket == id);

                if (ticket == null)
                    return Result<TicketEntity>.Failure("El ticket no fue encontrado.");

                var ticketEntity = new TicketEntity
                {
                    IdTicket = ticket.IdTicket,
                    Affair = ticket.Affair,
                    DateCreated = ticket.DateCreated,
                    DateAnswered = ticket.DateAnswered ?? null,
                    AnsweredBy = ticket.AnsweredBy ?? null,
                    ScalpAdmin = ticket.ScalpAdmin ?? false,
                    PersonId = ticket.PersonId,
                    StateId = ticket.StateId ?? null,
                    TypeId = ticket.TypeId ?? null,
                    Person = new PersonEntity
                    {
                        NameUser = ticket.Person.NameUser,
                        Email = ticket.Person.Email
                    },
                    State = ticket.State != null ? new CatStateEntity
                    {
                        NameState = ticket.State.NameState,
                    } : null,
                    Type = ticket.Type != null ? new CatTypeEntity
                    {
                        NameType = ticket.Type.NameType ?? string.Empty,
                    } : null
                };

                return Result<TicketEntity>.Success(ticketEntity);
            }
            catch (Exception ex)
            {
                return Result<TicketEntity>.Failure($"Error al obtener el ticket con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(TicketEntity entity)
        {
            try
            {
                var existingTicket = await _context.Tickets.FindAsync(entity.IdTicket);
                if (existingTicket == null)
                    return Result<bool>.Failure("El ticket no fue encontrado.");

                existingTicket.DateAnswered = DateUtility.DateNowColombia();
                existingTicket.AnsweredBy = entity.AnsweredBy;
                existingTicket.ScalpAdmin = entity.ScalpAdmin;
                existingTicket.StateId = entity.StateId;

                _context.Tickets.Update(existingTicket);
                int rowsAffected = await _context.SaveChangesAsync();

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el ticket: {ex.Message}");
            }
        }


        public Task<IEnumerable<TicketEntity>?> GetTicketsByDateAnswered(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketEntity>?> GetTicketsByDateCreated(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketEntity>?> GetTicketsByState(int stateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketEntity>?> GetTicketsByType(int typeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketEntity>?> GetTicketsScalpAdmin()
        {
            throw new NotImplementedException();
        }
    }
}