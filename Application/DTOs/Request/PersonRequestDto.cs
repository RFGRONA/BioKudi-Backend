using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Application.DTOs.Request
{
    public class PersonRequestDto
    {
        public string? NameUser { get; set; } = null!;
        public string? Telephone { get; set; }
        public string? Email { get; set; } = null!;
        public bool? EmailNotification { get; set; }
        public bool? EmailPost { get; set; }
        public string? Picture { get; set; }
    }

    public class ProfileRequestDto
    {
        public string? NameUser { get; set; } = null!;
        public string? Telephone { get; set; }
        public string? Email { get; set; } = null!;
        public bool? EmailNotification { get; set; }
        public bool? EmailPost { get; set; }
        public bool? EmailList { get; set; }
        public string? Picture { get; set; }
    }
}
