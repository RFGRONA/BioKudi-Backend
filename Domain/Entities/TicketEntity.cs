using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class TicketEntity
    {
        public int IdTicket { get; set; }
        public string Affair { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateAnswered { get; set; }
        public string AnsweredBy { get; set; }
        public bool ScalpAdmin { get; set; } = false;
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int? StateId { get; set; }
        public CatState State { get; set; }
        public int? TypeId { get; set; }
        public CatType Type { get; set; }
    }
}
