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

        /// <summary>
        /// Obtiene todas las reseñas.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<ReviewResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviewService.GetAllReviews();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene una reseña específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la reseña a obtener.</param>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ReviewResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _reviewService.GetReviewById(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Crea una nueva reseña.
        /// </summary>
        /// <param name="review">Los datos de la reseña a crear.</param>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReviewResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDto review)
        {
            var result = await _reviewService.CreateReview(review);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza una reseña existente.
        /// </summary>
        /// <param name="id">El ID de la reseña a actualizar.</param>
        /// <param name="review">Los nuevos datos de la reseña.</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewRequestDto review)
        {
            var result = await _reviewService.UpdateReview(id, review);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina una reseña por su ID.
        /// </summary>
        /// <param name="id">El ID de la reseña a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReview(id);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina una reseña por su ID mediante un administrador.
        /// </summary>
        /// <param name="id">El ID de la reseña a eliminar.</param>
        /// <param name="dto">Información adicional para la eliminación de la reseña.</param>
        [HttpDelete("/ReviewByAdmin/{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeleteReviewByAdmin(int id, [FromBody] ReviewDeleteByAdminDto dto)
        {
            var result = await _reviewService.DeleteReviewByAdmin(id, dto);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
