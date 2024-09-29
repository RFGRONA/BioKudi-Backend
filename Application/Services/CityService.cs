using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Application.Services
{
    public class CityService(CityMapping _cityMapping, ICityRepository _cityRepository) : ICityService
    {
        private readonly CityMapping _cityMapping = _cityMapping;
        private readonly ICityRepository _cityRepository = _cityRepository;
        public async Task<bool> CreateCity(CityRequestDto city)
        {
            try
            {
                var result = await _cityRepository.Create(_cityMapping.RequestToEntity(city));
                return result!=null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCity(int id)
        {
            try
            {
                var result = await _cityRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CityDto>?> GetCities()
        {
            try
            {
                var result = await _cityRepository.GetAll();
                return result.Select(city => _cityMapping.EntityToDto(city)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CityDto?> GetCityById(int id)
        {
            try
            {
                var result = await _cityRepository.GetById(id);
                return _cityMapping.EntityToDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCity(int id, CityRequestDto city)
        {
            try
            {
                var entity = _cityMapping.RequestToEntity(city);
                entity.IdCity = id;
                var result = await _cityRepository.Update(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
