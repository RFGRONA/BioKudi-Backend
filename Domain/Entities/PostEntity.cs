using Biokudi_Backend.Infrastructure.Data;

namespace Biokudi_Backend.Domain.Entities
{
    public class PostEntity
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Text { get; set; }
        public int? PlaceId { get; set; }
        public Place Place { get; set; }
        public ICollection<CatTagEntity> Tags { get; set; }
    }
}
