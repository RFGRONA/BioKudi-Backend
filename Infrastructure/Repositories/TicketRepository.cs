using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<TicketEntity>? Create(TicketEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TicketEntity>? GetById(int id)
        {
            throw new NotImplementedException();
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

        public Task<bool> Update(TicketEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
