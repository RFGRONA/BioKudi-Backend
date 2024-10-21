using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<ReviewResponseDto>> CreateReviewAsync(CreateReviewRequestDto dto)
        {
            var entity = ReviewMapping.ToEntity(dto);
            var result = await _reviewRepository.Create(entity);

            return result.IsSuccess
                ? Result<ReviewResponseDto>.Success(ReviewMapping.ToDto(result.Value))
                : Result<ReviewResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateReviewAsync(int id, UpdateReviewRequestDto dto)
        {
            var updatedEntity = ReviewMapping.ToEntity(dto);
            updatedEntity.IdReview = id;
            var result = await _reviewRepository.Update(updatedEntity);

            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteReviewAsync(int id)
        {
            var result = await _reviewRepository.Delete(id);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<ReviewResponseDto>> GetReviewByIdAsync(int id)
        {
            var result = await _reviewRepository.GetById(id);

            return result.IsSuccess
                ? Result<ReviewResponseDto>.Success(ReviewMapping.ToDto(result.Value))
                : Result<ReviewResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<IEnumerable<ReviewResponseDto>>> GetAllReviewsAsync()
        {
            var result = await _reviewRepository.GetAll();

            return result.IsSuccess && result.Value.Any()
                ? Result<IEnumerable<ReviewResponseDto>>.Success(result.Value.Select(ReviewMapping.ToDto))
                : Result<IEnumerable<ReviewResponseDto>>.Failure(result.ErrorMessage ?? "No se encontraron reseñas.");
        }

        public async Task<Result<IEnumerable<ReviewResponseDto>>> GetReviewsByPlaceIdAsync(int placeId)
        {
            var result = await _reviewRepository.GetReviewsByPlaceId(placeId);

            return result.IsSuccess && result.Value.Any()
                ? Result<IEnumerable<ReviewResponseDto>>.Success(result.Value.Select(ReviewMapping.ToDto))
                : Result<IEnumerable<ReviewResponseDto>>.Failure(result.ErrorMessage ?? "No se encontraron reseñas para este lugar.");
        }
    }
}
