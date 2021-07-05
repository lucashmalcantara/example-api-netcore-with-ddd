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
                .InclusiveBetween(from: 11, to: 98).WithMessage("Phone area code should be between 11 and 98");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Phone number should not be null or empty")
                .MinimumLength(8).WithMessage("Phone number must have at least 8 characters")
                .MaximumLength(9).WithMessage("Phone number must have a maximum of 9 characters")
                .Must(BeOnlyNumbers).WithMessage("Phone number must have only numbers");
        }

        private bool BeOnlyNumbers(string value) =>
            !string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^\\d+$");
    }
}
