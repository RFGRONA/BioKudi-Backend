namespace Biokudi_Backend.Application.DTOs.Request
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NameUser { get; set; }
        public string CaptchaToken { get; set; }
    }
}
