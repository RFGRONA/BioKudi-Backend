namespace Biokudi_Backend.Application.DTOs.Response
{
    public class ReviewResponseDto
    {
        public int IdReview { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public string DateCreated { get; set; } 
        public string DateModified { get; set; } 
        public int PersonId { get; set; }
        public string PersonName { get; set; } 
        public string PersonEmail { get; set; }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; } 
    }
}
