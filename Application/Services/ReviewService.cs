using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class ReviewService(IReviewRepository reviewRepository, ReviewMapping reviewMapping) : IReviewService
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly ReviewMapping _reviewMapping = reviewMapping;

        public async Task<Result<ReviewResponseDto>> CreateReview(CreateReviewRequestDto dto)
        {
            var entity = _reviewMapping.ToEntity(dto);
            var result = await _reviewRepository.Create(entity);

            return result.IsSuccess
                ? Result<ReviewResponseDto>.Success(_reviewMapping.ToDto(result.Value))
                : Result<ReviewResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateReview(int id, UpdateReviewRequestDto dto)
        {
            var updatedEntity = _reviewMapping.ToEntity(dto);
            updatedEntity.IdReview = id;
            var result = await _reviewRepository.Update(updatedEntity);

            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteReview(int id)
        {
            var result = await _reviewRepository.Delete(id);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<ReviewResponseDto>> GetReviewById(int id)
        {
            var result = await _reviewRepository.GetById(id);

            return result.IsSuccess
                ? Result<ReviewResponseDto>.Success(_reviewMapping.ToDto(result.Value))
                : Result<ReviewResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<IEnumerable<ReviewResponseDto>>> GetAllReviews()
        {
            var result = await _reviewRepository.GetAll();

            return result.IsSuccess && result.Value.Any()
                ? Result<IEnumerable<ReviewResponseDto>>.Success(result.Value.Select(_reviewMapping.ToDto))
                : Result<IEnumerable<ReviewResponseDto>>.Failure(result.ErrorMessage ?? "No se encontraron reseñas.");
        }

        public async Task<Result<IEnumerable<ReviewMapResponseDto>>> GetReviewsByPlaceId(int placeId)
        {
            var result = await _reviewRepository.GetReviewsByPlaceId(placeId);

            return result.IsSuccess && result.Value.Any()
                ? Result<IEnumerable<ReviewMapResponseDto>>.Success(result.Value.Select(_reviewMapping.ToReviewMapDto))
                : Result<IEnumerable<ReviewMapResponseDto>>.Failure(result.ErrorMessage ?? "No se encontraron reseñas para este lugar.");
        }
    }
}
