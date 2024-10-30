using Biokudi_Backend.Application.DTOs;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class ActivityRequestDtoValidator : AbstractValidator<ActivityRequestDto>
    {
        public ActivityRequestDtoValidator()
        {
            RuleFor(x => x.NameActivity)
                .NotEmpty().WithMessage("El nombre de la actividad es obligatorio.")
                .MaximumLength(128).WithMessage("El nombre de la actividad no debe exceder los 128 caracteres.");

            RuleFor(x => x.UrlIcon)
                .MaximumLength(15).WithMessage("El URL del icono no debe exceder los 15 caracteres.");
        }
    }
}
