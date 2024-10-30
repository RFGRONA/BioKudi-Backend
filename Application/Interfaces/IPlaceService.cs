using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPlaceService
    {
        Task<Result<List<StartCarrouselDto>>> GetStartCarrousel();
        Task<Result<List<PlaceListActivityDto>>> GetListActivities();
        Task<Result<List<PlaceListPointMapDto>>> GetListPointMap();
        Task<Result<PlaceDetailResponseDto>> GetPlaceById(int id);
        Task<Result<PlaceMapDetailResponseDto>> GetMapPlaceById(int id);
        Task<Result<List<PlaceListCrudDto>>> GetCrudPlaces();
        Task<Result<bool>> CreatePlace(PlaceRequestDto place);
        Task<Result<bool>> UpdatePlace(int id, PlaceRequestDto place);
        Task<Result<bool>> DeletePlace(int id);
    }
}
