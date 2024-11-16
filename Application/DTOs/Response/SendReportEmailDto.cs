namespace Biokudi_Backend.Application.DTOs.Response
{
    public class SendReportEmailDto
    {
        public string RecipientEmail { get; set; }
        public string TableName { get; set; }
        public string FileBase64 { get; set; }
    }
}
