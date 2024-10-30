namespace Biokudi_Backend.Application.DTOs
{
    public class RoleDto
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; } = null!;
    }

    public class RoleRequestDto
    {
        public string NameRole { get; set; } = null!;
    }

    public class IdRoleDto
    {
        public int IdRole { get; set; }
    }
}
