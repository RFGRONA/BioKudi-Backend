namespace Biokudi_Backend.Application.DTOs.Response
{
    public class StatusDto
    {
        public string Status { get; set; } = "Healthy"; 
        public string? Version { get; set; }
        public TimeSpan Uptime { get; set; }
        public bool DatabaseConnection { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
