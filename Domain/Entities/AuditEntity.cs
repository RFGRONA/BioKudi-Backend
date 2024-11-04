namespace Biokudi_Backend.Domain.Entities
{
    public class AuditEntity
    {
        public int IdAudit { get; set; }

        public string? ViewAction { get; set; }

        public string? Action { get; set; }

        public DateTime? Date { get; set; }

        public string? ModifiedBy { get; set; }

        public string? OldValue { get; set; }

        public string? PostValue { get; set; }
    }
}
