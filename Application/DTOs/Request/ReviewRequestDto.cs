namespace Biokudi_Backend.Application.DTOs.Request
{
    public class CreateReviewRequestDto
    {
        public decimal Rate { get; set; }
        public string? Comment { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }
    }

    public class UpdateReviewRequestDto
    {
        public decimal Rate { get; set; }
        public string? Comment { get; set; }
    }

}
