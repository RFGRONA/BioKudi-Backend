namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PlaceListPointMapDto
    {
        public int IdPlace { get; set; }
        public string NamePlace { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Link { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double Rating { get; set; }
    }
}
