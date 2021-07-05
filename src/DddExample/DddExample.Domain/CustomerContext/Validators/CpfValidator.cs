using DddExample.Domain.CustomerContext.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DddExample.Domain.CustomerContext.Validators
{
    public class CpfValidator : AbstractValidator<Cpf>
    {
        public CpfValidator()
        {
            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("CPF should not be empty")
                .Length(exactLength: 11).WithMessage("CPF must have 11 positions")
                .Must(BeOnlyNumbers).WithMessage("CPF must have only numbers");
        }

        private bool BeOnlyNumbers(string value) =>
            !string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^\\d+$");
    }
}
