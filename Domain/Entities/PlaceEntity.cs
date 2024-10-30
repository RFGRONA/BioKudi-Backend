using Biokudi_Backend.Infrastructure;
using System.Diagnostics;

namespace Biokudi_Backend.Domain.Entities
{
    public class PlaceEntity
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
        public int? StateId { get; set; }
        public CatCityEntity? City { get; set; }
        public CatStateEntity? State { get; set; }
        public ICollection<PictureEntity> Pictures { get; set; } = new List<PictureEntity>();
        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
        public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
        public ICollection<CatActivityEntity> Activities { get; set; } = new List<CatActivityEntity>();
        public ICollection<ListEntity> Lists { get; set; } = new List<ListEntity>();
        public double Rating { get; set; }
    }
}
