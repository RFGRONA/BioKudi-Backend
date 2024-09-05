using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PictureEntity
    {
        public int IdPicture { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int? TypeId { get; set; }
        public CatType Type { get; set; }
        public int? PlaceId { get; set; }
        public Place Place { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int? ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
