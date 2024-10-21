using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class PlaceMapping
    {
        private static readonly Random Random = new Random();
        private static readonly string[] DefaultImages = new[]
        {
            "https://i.postimg.cc/DZBXmNqB/camilo-ayala-FNZ2-Qc86-A0w-unsplash.jpg",
            "https://i.postimg.cc/zvv0fKRH/camilo-ayala-75li-Oyz-F2-HU-unsplash.jpg",
            "https://i.postimg.cc/L5Wvg5zC/daniel-restrepo-londono-CWNt01k-HFBE-unsplash.jpg",
            "https://i.postimg.cc/rwxgz8zj/diego-m-botero-hp2xt-VT-V4-unsplash.jpg",
            "https://i.postimg.cc/MGw5QhQS/jose-dominguez-GBMZKb-Fo1-Os-unsplash.jpg",
            "https://i.postimg.cc/XJ7g2dL1/david-hertle-1-D0-IXPsn3-BQ-unsplash.jpg",
            "https://i.postimg.cc/HxZ9FnPY/jaime-maldonado-w9i8-FPl-G7ls-unsplash.jpg",
            "https://i.postimg.cc/7h5n44pg/daniel-restrepo-londono-j-Lvj4-RNa-R-U-unsplash.jpg",
            "https://i.postimg.cc/zv4SXMr4/hector-orjuela-k-YSHh-ZGp9s-unsplash.jpg",
            "https://i.postimg.cc/wxrkVtmF/andres-f-uran-2m7-L-2-Mk-G1w-unsplash.jpg"
        };

        public StartCarrouselDto MapPlaceToCarrouselDto(PlaceEntity place)
        {
            return new StartCarrouselDto
            {
                name = place.NamePlace,
                rating = place.Rating,
                url = place.Pictures.FirstOrDefault()?.Link
                      ?? DefaultImages[Random.Next(DefaultImages.Length)]
            };
        }

        public PlaceListCrudDto PlaceToCrudList(PlaceEntity place)
        {
            return new PlaceListCrudDto
            {
                IdPlace = place.IdPlace,
                NamePlace = place.NamePlace,
                Link = place.Link,
                CityName = place.City?.NameCity,
                StateName = place.State?.NameState
            };
        }

        public PlaceDetailResponseDto MapToPlaceDetailResponseDto(PlaceEntity place)
        {
            return new PlaceDetailResponseDto
            {
                IdPlace = place.IdPlace,
                NamePlace = place.NamePlace,
                Link = place.Link,
                Address = place.Address,
                DateCreated = place.DateCreated,
                DateModified = place.DateModified,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Description = place.Description,
                CityName = place.City?.NameCity,
                CityId = place.City?.IdCity,
                StateId = place.State?.IdState,
                StateName = place.State?.NameState,
                Activities = place.Activities.Select(a => new ActivityDto
                {
                    IdActivity = a.IdActivity,
                    NameActivity = a.NameActivity,
                    UrlIcon = a.UrlIcon ?? string.Empty
                }).ToList()
            };
        }

        public PlaceEntity RequestToPlaceEntity(PlaceRequestDto place)
        {
            return new PlaceEntity
            {
                NamePlace = place.NamePlace,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Address = place.Address,
                Description = place.Description,
                Link = place.Link,
                StateId = place.StateId,
                CityId = place.CityId,
                Activities = place.Activities.Select(a => new CatActivityEntity
                {
                    IdActivity = a.IdActivity
                }).ToList(),
                Pictures = string.IsNullOrEmpty(place.Picture) ?
                    new List<PictureEntity>() : 
                    new List<PictureEntity> 
                    {
                        new PictureEntity
                        {
                            Link = place.Picture,
                            Name = place.NamePlace
                        }
                    }
            };
        }

        public PlaceListActivityDto MapToPlaceListActivityDto(PlaceEntity place)
        {
            return new PlaceListActivityDto
            {
                IdPlace = place.IdPlace,
                NamePlace = place.NamePlace,
                CityName = place.City?.NameCity,
                Description = place.Description,
                Image = place.Pictures.FirstOrDefault()?.Link
                      ?? DefaultImages[Random.Next(DefaultImages.Length)],
                Rating = place.Rating,
                Activities = place.Activities.Select(a => new ActivityDto
                {
                    IdActivity = a.IdActivity,
                    NameActivity = a.NameActivity,
                    UrlIcon = a.UrlIcon ?? string.Empty

                }).ToList()
            };
        }

        public PlaceListPointMapDto MapToPlaceListPointMapDto(PlaceEntity place)
        {
            return new PlaceListPointMapDto
            {
                IdPlace = place.IdPlace,
                NamePlace = place.NamePlace,
                Latitude = place.Latitude,
                Longitude = place.Longitude
            };
        }
    }
}
