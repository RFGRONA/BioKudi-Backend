namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PlaceListActivityDto
    {
        public int IdPlace { get; set; }
        public string NamePlace { get; set; } = null!;
        public string? CityName { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; } = null!;
        public string? Image { get; set; }
        public List<ActivityDto> Activities { get; set; } = null!;
    }
}
