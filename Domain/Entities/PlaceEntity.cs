using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PlaceEntity
    {
        public int IdPlace { get; set; }
        public string NamePlace { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        public int CityId { get; set; }
        public CatCity City { get; set; }
        public int StateId { get; set; }
        public CatState State { get; set; }
    }
}
