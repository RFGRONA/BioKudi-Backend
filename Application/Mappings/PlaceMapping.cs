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
    }
}
