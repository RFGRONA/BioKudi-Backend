using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class ListEntity
    {
        public int IdList { get; set; }
        public string NameList { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public ICollection<PlaceEntity> Places { get; set; }
    }
}
