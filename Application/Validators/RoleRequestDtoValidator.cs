using Biokudi_Backend.Application.DTOs;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class RoleRequestDtoValidator : AbstractValidator<RoleRequestDto>
    {
        public RoleRequestDtoValidator()
        {
            RuleFor(x => x.NameRole)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del rol no debe exceder los 50 caracteres.");
        }
    }
}
