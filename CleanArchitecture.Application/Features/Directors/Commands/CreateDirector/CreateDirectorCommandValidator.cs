using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("{Nombre} no admite valores nulos")
                .NotEmpty().WithMessage("{Nombre} no admite valores vacios");

            RuleFor(x => x.Apellido)
                .NotNull().WithMessage("{Apellido} no admite valores nulos")
                .NotEmpty().WithMessage("{Apellido} no admite valores vacios");
        }
    }
}
