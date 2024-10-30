using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es incorrecto.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.");

            RuleFor(x => x.NameUser)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(65).WithMessage("El nombre de usuario no debe exceder los 65 caracteres.");

            RuleFor(x => x.CaptchaToken)
                .NotEmpty().WithMessage("El captcha es obligatorio.");
        }
    }

}
