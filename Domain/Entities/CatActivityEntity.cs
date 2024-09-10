
namespace Biokudi_Backend.Domain.Entities
{
    public class CatActivityEntity
    {
        public int IdActivity { get; set; }
        public string NameActivity { get; set; }
        public string UrlIcon { get; set; }
        public ICollection<PlaceEntity> Places { get; set; }
    }
}
