using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PlaceDetailResponseDto
    {
        public int IdPlace { get; set; }
        public string NamePlace { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Link { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
    }
}
