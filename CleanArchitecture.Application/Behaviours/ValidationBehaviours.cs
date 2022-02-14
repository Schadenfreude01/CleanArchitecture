using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviours<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        public ValidationBehaviours(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(validators.Select(
                                                                        x => x.ValidateAsync(context, cancellationToken)
                                                                        )
                                                           );

                var failures = validationResults.SelectMany(x => x.Errors).Where(z => z != null).ToList();

                if(failures.Count != 0)
                {
                    throw new Exceptions.ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
