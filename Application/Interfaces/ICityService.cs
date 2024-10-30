using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface ICityService
    {
        Task<Result<List<CityDto>>> GetCities();
        Task<Result<CityDto>> GetCityById(int id);
        Task<Result<bool>> CreateCity(CityRequestDto city);
        Task<Result<bool>> UpdateCity(int id, CityRequestDto city);
        Task<Result<bool>> DeleteCity(int id);
    }
}
