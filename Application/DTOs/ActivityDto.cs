namespace Biokudi_Backend.Application.DTOs
{
    public class ActivityDto
    {
        public int IdActivity { get; set; }
        public string NameActivity { get; set; } = null!;
        public string UrlIcon { get; set; } = null!;
    }

    public class ActivityRequestDto
    {
        public string NameActivity { get; set; } = null!;
        public string UrlIcon { get; set; } = null!;
    }

    public class IdActivityDto
    {
        public int IdActivity { get; set; }
    }
}
