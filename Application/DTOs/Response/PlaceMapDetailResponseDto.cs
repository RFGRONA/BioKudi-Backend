using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.DTOs.Response
{
    public class PlaceMapDetailResponseDto
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
        public CatCityEntity? City { get; set; }
        public CatStateEntity? State { get; set; }
        public ICollection<PictureResponseDto> Pictures { get; set; } = [];
        public ICollection<ReviewMapResponseDto> Reviews { get; set; } = [];
        public ICollection<ActivityDto> Activities { get; set; } = [];
        public double Rating { get; set; }
    }
}
