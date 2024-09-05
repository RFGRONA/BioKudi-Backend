using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PlaceListEntity
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int ListId { get; set; }
        public List List { get; set; }
    }
}
