namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PersonCrudResponseDto
    {
        public string NameUser { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
