using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace DddExample.Domain.Core.Results.Extensions
{
    public static class ResultConverterExtensions
    {
        public static SimpleResult ToErrorResult(this IList<ValidationFailure> failures)
        {
            var errors = ConvertToDomainError(failures);
            return SimpleResult.Error(errors);
        }

        public static Result<T> ToErrorResult<T>(this IList<ValidationFailure> failures)
        {
            var errors = ConvertToDomainError(failures);
            return Result<T>.Error(errors);
        }

        private static IReadOnlyCollection<Error> ConvertToDomainError(IList<ValidationFailure> failures) =>
            failures.Select(f => new Error(f.PropertyName, f.ErrorMessage)).ToList();
    }
}
