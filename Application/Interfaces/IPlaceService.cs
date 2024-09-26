using Biokudi_Backend.Application.DTOs.Response;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPlaceService
    {
        Task<List<StartCarrouselDto>?> GetStartCarrousel();
    }
}
