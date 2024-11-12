using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class PlaceService(IPlaceRepository _placeRepository, PlaceMapping _placeMapping) : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository = _placeRepository;
        private readonly PlaceMapping _placeMapping = _placeMapping;

        public async Task<Result<bool>> CreatePlace(PlaceRequestDto place)
        {
            var result = await _placeRepository.Create(_placeMapping.RequestToPlaceEntity(place));
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeletePlace(int id)
        {
            var result = await _placeRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<PlaceListCrudDto>>> GetCrudPlaces()
        {
            var result = await _placeRepository.GetAll();
            if (result.IsSuccess)
            {
                var places = result.Value
                    .OrderBy(p => p.IdPlace)
                    .Select(p => _placeMapping.PlaceToCrudList(p))
                    .ToList();

                return Result<List<PlaceListCrudDto>>.Success(places);
            }
            return Result<List<PlaceListCrudDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<PlaceListActivityDto>>> GetListActivities()
        {
            var result = await _placeRepository.GetAll();
            if (result.IsSuccess)
            {
                var places = result.Value
                    .OrderBy(p => p.NamePlace)
                    .Select(p => _placeMapping.MapToPlaceListActivityDto(p))
                    .ToList();

                return Result<List<PlaceListActivityDto>>.Success(places);
            }
            return Result<List<PlaceListActivityDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<PlaceListPointMapDto>>> GetListPointMap()
        {
            var result = await _placeRepository.GetAll();
            if (result.IsSuccess)
            {
                var places = result.Value
                    .Select(p => _placeMapping.MapToPlaceListPointMapDto(p))
                    .ToList();

                return Result<List<PlaceListPointMapDto>>.Success(places);
            }
            return Result<List<PlaceListPointMapDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<PlaceDetailResponseDto>> GetPlaceById(int id)
        {
            var result = await _placeRepository.GetById(id);
            if (result.IsSuccess)
            {
                return Result<PlaceDetailResponseDto>.Success(_placeMapping.MapToPlaceDetailResponseDto(result.Value));
            }
            return Result<PlaceDetailResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<PlaceMapDetailResponseDto>> GetMapPlaceById(int id)
        {
            var result = await _placeRepository.GetById(id);
            if (result.IsSuccess)
            {
                return Result<PlaceMapDetailResponseDto>.Success(_placeMapping.ToPlaceMapDetailResponseDto(result.Value));
            }
            return Result<PlaceMapDetailResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<StartCarrouselDto>>> GetStartCarrousel()
        {
            var result = await _placeRepository.GetAll();

            if (!result.IsSuccess || result.Value.Count() < 7)
            {
                return Result<List<StartCarrouselDto>>.Failure("No hay suficientes lugares para mostrar en el carrusel.");
            }

            var startCarrousel = result.Value
                .OrderByDescending(p => p.Rating)  
                .ThenBy(p => p.NamePlace)           
                .Take(10)
                .Select(p => (StartCarrouselDto)_placeMapping.MapPlaceToCarrouselDto(p))
                .ToList();

            return Result<List<StartCarrouselDto>>.Success(startCarrousel);
        }

        public async Task<Result<bool>> UpdatePlace(int id, PlaceRequestDto place)
        {
            var entity = _placeMapping.RequestToPlaceEntity(place);
            entity.IdPlace = id;

            var result = await _placeRepository.Update(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<PlaceListActivityDto>>> SearchPlaces(PlaceSearchRequestDto request)
        {
            var result = await _placeRepository.SearchPlaces(request.Search);
            if (result.IsSuccess)
            {
                var places = result.Value
                    .Select(p => _placeMapping.MapToPlaceListActivityDto(p))
                    .ToList();

                return Result<List<PlaceListActivityDto>>.Success(places);
            }
            return Result<List<PlaceListActivityDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<PlaceMapDetailResponseDto>> GetMapRandomPlace()
        {
            var allPlacesResult = await _placeRepository.GetAll();

            if (!allPlacesResult.IsSuccess || !allPlacesResult.Value.Any())
                return Result<PlaceMapDetailResponseDto>.Failure(allPlacesResult.ErrorMessage ?? "No hay lugares disponibles.");

            var random = new Random();
            var randomPlace = allPlacesResult.Value.ElementAt(random.Next(allPlacesResult.Value.Count()));
            var placeMapDetailResponse = _placeMapping.ToPlaceMapDetailResponseDto(randomPlace);
            return Result<PlaceMapDetailResponseDto>.Success(placeMapDetailResponse);
        }
    }
}