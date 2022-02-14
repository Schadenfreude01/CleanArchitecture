using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("Se presentaron uno o mas errores de validacion")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(x => x.PropertyName, 
                                      x => x.ErrorMessage)
                             .ToDictionary(failureGroup => failureGroup.Key, 
                                           failureGroup => failureGroup.ToArray()
                                      );
        }

        public IDictionary<string, string[]> Errors { get; private set; }
    }
}
