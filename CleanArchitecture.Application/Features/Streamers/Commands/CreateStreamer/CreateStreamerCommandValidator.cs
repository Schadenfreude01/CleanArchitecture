﻿using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("{Nombre}, no puede estar en blanco.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Nombre}, no puede exceder los 50 caracteres.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("La {Url}, no puede estar en blanco.");
        }
    }
}
