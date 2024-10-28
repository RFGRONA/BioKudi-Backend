namespace Biokudi_Backend.Application.DTOs.Request
{
    public class TicketCreateRequestDto
    {
        public string Affair { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
    }

    public class TicketUpdateRequestDto
    {
        public string? Response { get; set; }
        public string? AnsweredBy { get; set; }
        public int? StateId { get; set; }
        public string Email { get; set; }
    }

    public class ScalpTicketRequestDto
    {
        public bool ScalpAdmin { get; set; }
    }
}