using System.Collections.Generic;
using System.Linq;

namespace DddExample.Domain.Core
{
    public class Result<T>
    {
        public bool HasError { get { return Errors.Any(); } }
        public IReadOnlyCollection<Error> Errors { get; private set; }
        public T Data { get; }

        private Result(T obj)
        {
            Data = obj;
            Errors = new List<Error>();
        }

        private Result(IReadOnlyCollection<Error> errors)
        {
            Data = default;
            Errors = errors;
        }

        public static Result<T> Ok(T obj) => new Result<T>(obj);

        public static Result<T> Error(IReadOnlyCollection<Error> errors) =>
            new Result<T>(errors);

        public static Result<T> Error(Error error) =>
            new Result<T>(new List<Error> { error });
    }
}
