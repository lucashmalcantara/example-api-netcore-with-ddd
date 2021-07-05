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
                .NotEmpty().WithMessage("Birthdate should have a value")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birthdate date should not be in the future");
        }
    }
}
