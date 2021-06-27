using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.ValueObjects;
using FluentValidation;
using System;

namespace DddExample.Domain.CustomerContext.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).SetValidator(new PersonNameValidator());
            RuleFor(x => x.Cpf).SetValidator(new CpfValidator());
            RuleFor(x => x.Email).SetValidator(new EmailValidator());
            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("The name should have a value")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("The birthdate date should not be in the future");
        }
    }
}
