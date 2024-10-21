using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class PersonCrudRequestDtoValidator : AbstractValidator<PersonCrudRequestDto>
    {
        public PersonCrudRequestDtoValidator()
        {
            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("El ID del rol debe ser mayor que cero.");
        }
    }
}
