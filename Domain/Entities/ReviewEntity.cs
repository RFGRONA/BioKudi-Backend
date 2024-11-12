using Biokudi_Backend.Infrastructure.Data;

namespace Biokudi_Backend.Domain.Entities
{
    public class ReviewEntity
    {
        public int IdReview { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
