using System.Collections.Generic;
using System.Linq;

namespace DddExample.Domain.Core
{
    public class SimpleResult
    {
        public bool HasError { get { return Errors.Any(); } }
        public IReadOnlyCollection<Error> Errors { get; private set; }

        private SimpleResult()
        {
            Errors = new List<Error>();
        }

        private SimpleResult(IReadOnlyCollection<Error> errors)
        {
            Errors = errors;
        }

        public static SimpleResult Ok()
        {
            return new SimpleResult();
        }

        public static SimpleResult Error(IReadOnlyCollection<Error> errors)
        {
            return new SimpleResult(errors);
        }

        public static SimpleResult Error(Error error)
        {
            return new SimpleResult(new List<Error> { error });
        }
    }
}
