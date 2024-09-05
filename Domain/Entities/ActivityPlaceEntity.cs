using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class ActivityPlaceEntity
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int ActivityId { get; set; }
        public CatActivity Activity { get; set; }
    }
}
