using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class ReviewMapping
    {
        public static ReviewEntity ToEntity(CreateReviewRequestDto dto)
        {
            return new ReviewEntity
            {
                Rate = dto.Rate,
                Comment = dto.Comment ?? string.Empty,
                PersonId = dto.PersonId,
                PlaceId = dto.PlaceId,
            };
        }

        public static ReviewEntity ToEntity(UpdateReviewRequestDto dto)
        {
            return new ReviewEntity
            {
                Rate = dto.Rate,
                Comment = dto.Comment ?? string.Empty
            };
        }

        public static ReviewResponseDto ToDto(ReviewEntity entity)
        {
            return new ReviewResponseDto
            {
                IdReview = entity.IdReview,
                Rate = entity.Rate,
                Comment = entity.Comment,
                DateCreated = entity.DateCreated.ToString("yyyy-MM-dd"),
                DateModified = entity.DateModified?.ToString("yyyy-MM-dd") ?? string.Empty,
                PersonId = entity.PersonId,
                PersonName = entity.Person.NameUser,
                PlaceId = entity.PlaceId,
                PlaceName = entity.Place.NamePlace 
            };
        }
    }

}
