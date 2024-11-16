namespace Biokudi_Backend.Application.DTOs
{
    public class AuditReportDto
    {
        public int TotalRecords { get; set; }
        public Dictionary<string, int> ActionCounts { get; set; } = new Dictionary<string, int>();
        public List<WeeklyActivity> WeeklyActivityData { get; set; } = new List<WeeklyActivity>();
        public List<AuditDto> AuditRecords { get; set; } = new List<AuditDto>();
    }

    public class AuditDto
    {
        public int IdAudit { get; set; }
        public string? ViewAction { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public string? OldValue { get; set; }
        public string? PostValue { get; set; }
    }

    public class WeeklyActivity
    {
        public DateOnly Date { get; set; }
        public int InsertCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
    }
}
