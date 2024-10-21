using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PlaceListCrudDto
    {
        public int IdPlace { get; set; }
        public string NamePlace { get; set; } = null!;
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? Link { get; set; }
    }
}
