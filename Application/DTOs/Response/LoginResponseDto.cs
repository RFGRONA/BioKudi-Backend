namespace Biokudi_Backend.Application.DTOs.Response
{
    public class LoginResponseDto
    {
        public string NameUser { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ProfilePicture { get; set; }
    }
}
