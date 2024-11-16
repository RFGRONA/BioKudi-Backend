namespace Biokudi_Backend.Application.DTOs.Request
{
    public class UpdatePasswordRequestDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
