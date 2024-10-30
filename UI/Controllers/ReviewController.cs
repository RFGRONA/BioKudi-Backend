using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<ReviewResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviewService.GetAllReviews();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ReviewResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _reviewService.GetReviewById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReviewResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDto review)
        {
            var result = await _reviewService.CreateReview(review);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewRequestDto review)
        {
            var result = await _reviewService.UpdateReview(id, review);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReview(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
