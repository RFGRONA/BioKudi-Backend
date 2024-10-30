using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PictureEntity
    {
        public int IdPicture { get; set; }

        public string Name { get; set; } = null!;

        public string Link { get; set; } = null!;

        public DateTime? DateCreated { get; set; }

        public int? TypeId { get; set; }

        public int? PlaceId { get; set; }

        public int? PersonId { get; set; }

        public int? TicketId { get; set; }

        public int? ReviewId { get; set; }

        public virtual PersonEntity? Person { get; set; }

        public virtual PlaceEntity? Place { get; set; }

        public virtual ReviewEntity? Review { get; set; }

        public virtual TicketEntity? Ticket { get; set; }

        public virtual CatTypeEntity? Type { get; set; }
    }
}
