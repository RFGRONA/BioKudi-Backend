namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PersonListCrudDto
    {
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
    }
}
