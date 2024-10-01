namespace Biokudi_Backend.Application.DTOs
{
    public class CityDto
    {
        public int IdCity { get; set; }
        public string NameCity { get; set; }
        public int? IdDepartment { get; set; }
        public string? DepartmentCity { get; set; }
    }

    public class CityRequestDto
    {
        public string NameCity { get; set; }
        public int? IdDepartment { get; set; }
    }
}
