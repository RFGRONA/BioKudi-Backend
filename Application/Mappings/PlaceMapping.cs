using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class PlaceMapping
    {
        public StartCarrouselDto MapPlaceToCarrouselDto(PlaceEntity place)
        {
            return new StartCarrouselDto
            {
                name = place.NamePlace,
                rating = place.Rating,
                url = place.Pictures.FirstOrDefault()?.Link
                      ?? "https://i.postimg.cc/8kHXP1bz/dan-gold-Uuh0-K9-Yt-NM8-unsplash-1.jpg"
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
                StateName = place.State?.NameState
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
                }).ToList()
            };
        }
    }
}
