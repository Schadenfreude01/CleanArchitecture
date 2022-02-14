using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(x => x.Nombre)
                //.NotEmpty().WithMessage("{Nombre}, no permite vacios")
                .NotNull().WithMessage("{Nombre}, no permite nulos");

            RuleFor(x => x.Url)
                //.NotEmpty().WithMessage("{Url}, no permite vacios")
                .NotNull().WithMessage("{Url}, no permite nulos");
        }
    }
}
