using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IReviewService
    {
        Task<Result<ReviewResponseDto>> CreateReviewAsync(CreateReviewRequestDto dto);
        Task<Result<bool>> UpdateReviewAsync(int id, UpdateReviewRequestDto dto);
        Task<Result<bool>> DeleteReviewAsync(int id);
        Task<Result<ReviewResponseDto>> GetReviewByIdAsync(int id);
        Task<Result<IEnumerable<ReviewResponseDto>>> GetAllReviewsAsync();
        Task<Result<IEnumerable<ReviewResponseDto>>> GetReviewsByPlaceIdAsync(int placeId);
    }
}
