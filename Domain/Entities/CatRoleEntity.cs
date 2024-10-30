using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class CatRoleEntity
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
        public ICollection<PersonEntity> People { get; set; } = new List<PersonEntity>();
    }
}
