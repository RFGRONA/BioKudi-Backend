namespace Biokudi_Backend.Application.DTOs.Response
{
    public class TicketResponseDto
    {
        public int IdTicket { get; set; }
        public string Affair { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateAnswered { get; set; }
        public string? AnsweredBy { get; set; }
        public bool ScalpAdmin { get; set; } = false;
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? PersonEmail { get; set; }
        public int? StateId { get; set; }
        public int? TypeId { get; set; }
    }
}