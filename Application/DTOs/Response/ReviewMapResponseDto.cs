namespace Biokudi_Backend.Application.DTOs.Response
{
    public class ReviewMapResponseDto
    {
        public int IdReview { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public string PersonName { get; set; }
        public int PersonId { get; set; }
    }
}
