using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class PlaceRequestDtoValidator : AbstractValidator<PlaceRequestDto>
    {
        public PlaceRequestDtoValidator()
        {
            RuleFor(x => x.NamePlace)
                .NotEmpty().WithMessage("El nombre del lugar es obligatorio.")
                .MaximumLength(80).WithMessage("El nombre del lugar no debe exceder los 80 caracteres.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("La latitud debe estar entre -90 y 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("La longitud debe estar entre -180 y 180.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(128).WithMessage("La dirección no debe exceder los 128 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(560).WithMessage("La descripción no debe exceder los 560 caracteres.");

            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("El enlace es obligatorio.")
                .MaximumLength(255).WithMessage("El enlace no debe exceder los 255 caracteres.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).When(x => x.CityId.HasValue).WithMessage("El ID de la ciudad debe ser mayor que cero.");

            RuleFor(x => x.StateId)
                .GreaterThan(0).When(x => x.StateId.HasValue).WithMessage("El ID del estado debe ser mayor que cero.");
        }
    }

}
