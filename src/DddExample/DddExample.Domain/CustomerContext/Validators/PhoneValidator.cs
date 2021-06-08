using DddExample.Domain.CustomerContext.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DddExample.Domain.CustomerContext.Validators
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.AreaCode)
                .InclusiveBetween(from: 11, to: 98).WithMessage("The phone area code should be between 11 and 98");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("The phone number should not be null or empty")
                .MinimumLength(8).WithMessage("The phone number must have at least 8 characters")
                .MaximumLength(9).WithMessage("The phone number must have a maximum of 9 characters")
                .Must(BeOnlyNumbers).WithMessage("The phone number must have only numbers");
        }

        private bool BeOnlyNumbers(string value) => Regex.IsMatch(value, "^\\d+");
    }
}
