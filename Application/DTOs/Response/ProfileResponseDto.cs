namespace Biokudi_Backend.Application.DTOs.Response
{
    public class ProfileResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public bool EmailNotification { get; set; } 
        public bool EmailPost { get; set; } 

    }
}
