namespace Biokudi_Backend.Application.DTOs.Request
{
    public class PlaceRequestDto
    {
        public string NamePlace { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Link { get; set; } = null!;
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public string? Picture { get; set; }
        public List<IdActivityDto> Activities { get; set; } = null!;
    }
}
