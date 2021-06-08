using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.ValueObjects;
using FluentValidation;
using System;

namespace DddExample.Domain.CustomerContext.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator(
            IValidator<PersonName> personNameValidator,
            IValidator<Cpf> cpfValidator,
            IValidator<Email> emailValidator)
        {
            RuleFor(x => x.Name).SetValidator(personNameValidator);
            RuleFor(x => x.Cpf).SetValidator(cpfValidator);
            RuleFor(x => x.Email).SetValidator(emailValidator);
            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("The name should have a value")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("The birthdate date should not be in the future");
        }
    }
}
