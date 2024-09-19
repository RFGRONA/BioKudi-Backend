namespace Biokudi_Backend.Application.DTOs.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string NameUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
