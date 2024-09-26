namespace Biokudi_Backend.Application.DTOs.Request
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean RememberMe { get; set; }
        public string CaptchaToken { get; set; }
    }
}
