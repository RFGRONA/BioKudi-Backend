using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Infrastructure.Repositories;
using Biokudi_Backend.Application.DTOs.Request;

namespace Biokudi_Backend.Application.Services
{
    public class PlaceService(IPlaceRepository _placeRepository, PlaceMapping _placeMapping) : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository = _placeRepository;
        private readonly PlaceMapping _placeMapping = _placeMapping;

        public async Task<bool?> CreatePlace(PlaceRequestDto place)
        {
            try
            {
                var result = await _placeRepository.Create(_placeMapping.RequestToPlaceEntity(place));
                if (result != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePlace(int id)
        {
            try
            {
                var result = await _placeRepository.Delete(id);
                if (result)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PlaceListCrudDto>?> GetCrudPlaces()
        {
            try
            {
                var result = await _placeRepository.GetAll();
                var places = result
                .OrderBy(p => p.IdPlace)
                .Select(p => _placeMapping.PlaceToCrudList(p))
                .ToList() ?? null;
                return places;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlaceDetailResponseDto?> GetPlaceById(int id)
        {
            try
            {
                var result = await _placeRepository.GetById(id) ?? null;
                return _placeMapping.MapToPlaceDetailResponseDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public async Task<bool?> UpdatePlace(int id, PlaceRequestDto place)
        {
            try
            {
                var entity = _placeMapping.RequestToPlaceEntity(place);
                entity.IdPlace = id;
                var result = await _placeRepository.Update(entity);
                if (result != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
