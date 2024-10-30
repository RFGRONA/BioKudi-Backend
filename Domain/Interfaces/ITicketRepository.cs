using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface ITicketRepository : IRepository<TicketEntity>
    {
        Task<IEnumerable<TicketEntity>?> GetTicketsByState(int stateId);
        Task<IEnumerable<TicketEntity>?> GetTicketsByType(int typeId);
        Task<IEnumerable<TicketEntity>?> GetTicketsScalpAdmin();
        Task<IEnumerable<TicketEntity>?> GetTicketsByDateCreated(DateTime date);
        Task<IEnumerable<TicketEntity>?> GetTicketsByDateAnswered(DateTime date);
    }
}
