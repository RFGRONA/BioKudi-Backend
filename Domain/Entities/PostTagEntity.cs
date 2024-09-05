using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PostTagEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int TagId { get; set; }
        public CatTag Tag { get; set; }
    }
}
