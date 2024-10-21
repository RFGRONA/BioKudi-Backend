using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class CreateReviewRequestDtoValidator : AbstractValidator<CreateReviewRequestDto>
    {
        public CreateReviewRequestDtoValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5).WithMessage("La calificación debe estar entre 1 y 5.");

            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("El ID de la persona es obligatorio.");

            RuleFor(x => x.PlaceId)
                .GreaterThan(0).WithMessage("El ID del lugar es obligatorio.");
        }
    }

}
