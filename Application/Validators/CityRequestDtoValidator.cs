using Biokudi_Backend.Application.DTOs;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class CityRequestDtoValidator : AbstractValidator<CityRequestDto>
    {
        public CityRequestDtoValidator()
        {
            RuleFor(x => x.NameCity)
                .NotEmpty().WithMessage("El nombre de la ciudad es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre de la ciudad no debe exceder los 60 caracteres.");

            RuleFor(x => x.IdDepartment)
                .GreaterThan(0).When(x => x.IdDepartment.HasValue).WithMessage("El ID del departamento debe ser mayor que cero.");
        }
    }
}
