using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Application.Services
{
    public class CityService(CityMapping _cityMapping, ICityRepository _cityRepository) : ICityService
    {
        private readonly CityMapping _cityMapping = _cityMapping;
        private readonly ICityRepository _cityRepository = _cityRepository;

        public async Task<Result<bool>> CreateCity(CityRequestDto city)
        {
            var result = await _cityRepository.Create(_cityMapping.RequestToEntity(city));
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteCity(int id)
        {
            return await _cityRepository.Delete(id);
        }

        public async Task<Result<List<CityDto>>> GetCities()
        {
            var result = await _cityRepository.GetAll();
            return result.IsSuccess
                ? Result<List<CityDto>>.Success(result.Value.Select(city => _cityMapping.EntityToDto(city)).ToList())
                : Result<List<CityDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<CityDto>> GetCityById(int id)
        {
            var result = await _cityRepository.GetById(id);
            return result.IsSuccess
                ? Result<CityDto>.Success(_cityMapping.EntityToDto(result.Value))
                : Result<CityDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateCity(int id, CityRequestDto city)
        {
            var entity = _cityMapping.RequestToEntity(city);
            entity.IdCity = id;
            return await _cityRepository.Update(entity);
        }
    }
}
