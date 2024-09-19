using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Application.Mappings;

namespace Biokudi_Backend.Application.Services
{
    public class PlaceService(IPlaceRepository _placeRepository, PlaceMapping _placeMapping) : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository = _placeRepository;
        private readonly PlaceMapping _placeMapping = _placeMapping;

        public async Task<List<StartCarrouselDto>?> GetStartCarrousel()
        {
            var places = await _placeRepository.GetAll();

            if (places == null || places.Count() < 7)
            {
                return null;
            }
            var startCarrousel = places
            .OrderByDescending(p => p.Rating)
            .Take(10)
            .Select(p => (StartCarrouselDto)_placeMapping.MapPlaceToCarrouselDto(p)) 
            .ToList();

            return startCarrousel;
        }

    }
}
