using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.DTOs.Request;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPlaceService
    {
        Task<List<StartCarrouselDto>?> GetStartCarrousel();
        Task<PlaceDetailResponseDto?> GetPlaceById(int id);
        Task<List<PlaceListCrudDto>?> GetCrudPlaces();
        Task<bool?> CreatePlace(PlaceRequestDto place);
        Task<bool?> UpdatePlace(int id, PlaceRequestDto place);
        Task<bool> DeletePlace(int id);
    }
}
