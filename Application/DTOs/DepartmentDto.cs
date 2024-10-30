namespace Biokudi_Backend.Application.DTOs
{
    public class DepartmentDto
    {
        public int IdDepartment { get; set; }
        public string NameDepartment { get; set; }
    }

    public class DepartmentRequestDto
    {
        public string NameDepartment { get; set; }
    }
}
