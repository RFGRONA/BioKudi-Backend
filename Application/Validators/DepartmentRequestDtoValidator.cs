using Biokudi_Backend.Application.DTOs;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class DepartmentRequestDtoValidator : AbstractValidator<DepartmentRequestDto>
    {
        public DepartmentRequestDtoValidator()
        {
            RuleFor(x => x.NameDepartment)
                .NotEmpty().WithMessage("El nombre del departamento es obligatorio.")
                .MaximumLength(40).WithMessage("El nombre del departamento no debe exceder los 40 caracteres.");
        }
    }
}
