using Biokudi_Backend.Application.DTOs.Request;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es incorrecto.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");

            RuleFor(x => x.CaptchaToken)
                .NotEmpty().WithMessage("El captcha es obligatorio.");
        }
    }
}
