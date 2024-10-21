using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class UpdateReviewRequestDtoValidator : AbstractValidator<UpdateReviewRequestDto>
    {
        public UpdateReviewRequestDtoValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5).WithMessage("La calificación debe estar entre 1 y 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(255).WithMessage("El comentario no debe exceder los 255 caracteres.");
        }
    }
}
