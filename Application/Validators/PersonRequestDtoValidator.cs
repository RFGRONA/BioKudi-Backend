using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class PersonRequestDtoValidator : AbstractValidator<PersonRequestDto>
    {
        public PersonRequestDtoValidator()
        {
            RuleFor(x => x.NameUser)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(65).WithMessage("El nombre de usuario no debe exceder los 65 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es incorrecto.")
                .MaximumLength(75).WithMessage("El correo electrónico no debe exceder los 75 caracteres.");

            RuleFor(x => x.Telephone)
                .MaximumLength(15).WithMessage("El número de teléfono no debe exceder los 15 caracteres.")
                .Matches(@"^\d+$").WithMessage("El número de teléfono debe contener solo dígitos.");
        }
    }

}
