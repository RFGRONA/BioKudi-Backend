using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IReviewService
    {
        Task<Result<ReviewResponseDto>> CreateReview(CreateReviewRequestDto dto);
        Task<Result<bool>> UpdateReview(int id, UpdateReviewRequestDto dto);
        Task<Result<bool>> DeleteReview(int id);
        Task<Result<ReviewResponseDto>> GetReviewById(int id);
        Task<Result<IEnumerable<ReviewResponseDto>>> GetAllReviews();
        Task<Result<IEnumerable<ReviewMapResponseDto>>> GetReviewsByPlaceId(int placeId);
    }
}
