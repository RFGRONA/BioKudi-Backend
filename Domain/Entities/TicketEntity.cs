namespace Biokudi_Backend.Domain.Entities
{
    public class TicketEntity
    {
        public int IdTicket { get; set; }
        public string Affair { get; set; }
        public DateTime DateCreated { get; set; } 
        public DateTime? DateAnswered { get; set; }
        public string? AnsweredBy { get; set; }
        public bool ScalpAdmin { get; set; } = false;
        public int PersonId { get; set; }
        public PersonEntity? Person { get; set; }
        public int? StateId { get; set; }
        public CatStateEntity? State { get; set; }
        public int? TypeId { get; set; }
        public CatTypeEntity? Type { get; set; }
    }
}