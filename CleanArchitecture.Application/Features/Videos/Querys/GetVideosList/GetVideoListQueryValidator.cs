using FluentValidation;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideoListQueryValidator : AbstractValidator<GetVideoListQuery>
    {
        public GetVideoListQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("{UserName}, no permite vacios")
                .NotNull().WithMessage("{UserName}, no permite nulos");
        }
    }
}
