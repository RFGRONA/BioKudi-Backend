using Biokudi_Backend.Application.DTOs;
using FluentValidation;

namespace Biokudi_Backend.Application.Validators
{
    public class StateRequestDtoValidator : AbstractValidator<StateRequestDto>
    {
        public StateRequestDtoValidator()
        {
            RuleFor(x => x.NameState)
                .NotEmpty().WithMessage("El nombre del estado es obligatorio.")
                .MaximumLength(30).WithMessage("El nombre del estado no debe exceder los 30 caracteres.");

            RuleFor(x => x.TableRelation)
                .MaximumLength(30).When(x => x.TableRelation != null).WithMessage("El nombre de la tabla relacionada no debe exceder los 30 caracteres.");
        }
    }
}
