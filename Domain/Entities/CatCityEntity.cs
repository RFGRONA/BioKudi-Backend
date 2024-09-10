using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class CatCityEntity
    {
        public int IdCity { get; set; }
        public string NameCity { get; set; }
        public int DepartmentId { get; set; }
        public CatDepartment Department { get; set; }
    }
}
