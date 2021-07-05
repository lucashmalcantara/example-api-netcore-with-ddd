using DddExample.Domain.CustomerContext.ValueObjects;
using FluentValidation;

namespace DddExample.Domain.CustomerContext.Validators
{
    public class PersonNameValidator : AbstractValidator<PersonName>
    {
        public PersonNameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name should not be null or empty")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long");

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage("Last name should not be null or empty")
               .MinimumLength(2).WithMessage("Last name must be at least 2 characters long");
        }
    }
}
