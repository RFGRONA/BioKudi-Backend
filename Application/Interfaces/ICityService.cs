using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDto>?> GetCities();
        Task<CityDto?> GetCityById(int id);
        Task<bool> CreateCity(CityRequestDto city);
        Task<bool> UpdateCity(int id, CityRequestDto city);
        Task<bool> DeleteCity(int id);
    }
}
