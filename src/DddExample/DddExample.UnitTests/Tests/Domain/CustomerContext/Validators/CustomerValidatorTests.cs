using DddExample.Domain.CustomerContext.Validators;
using DddExample.Tests.Shared.Builders.Domain.CustomerContext.Entities;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Validators
{
    public class CustomerValidatorTests : UnitTestBase
    {
        [Test]
        public async Task Validation_should_success_when_all_properties_are_valid()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var validCustomer = customerBuilder.Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(validCustomer);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public async Task Validation_should_fail_when_Birthdate_has_default_DateTime_value()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var invalidCustomer = customerBuilder.WithBirthdate(default).Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(invalidCustomer);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Birthdate should have a value");
        }

        [Test]
        public async Task Validation_should_fail_when_Birthdate_is_in_the_future()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(30);

            var customerBuilder = new CustomerBuilder();
            var invalidCustomer = customerBuilder.WithBirthdate(futureDate).Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(invalidCustomer);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Birthdate date should not be in the future");
        }

        [Test]
        public async Task Validation_should_fail_when_Name_is_invalid()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var invalidCustomer = customerBuilder.WithInvalidPersonName().Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(invalidCustomer);

            // Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Test]
        public async Task Validation_should_fail_when_Cpf_is_invalid()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var invalidCustomer = customerBuilder.WithInvalidCpf().Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(invalidCustomer);

            // Assert
            validationResult.IsValid.Should().BeFalse();
        }

        [Test]
        public async Task Validation_should_fail_when_Email_is_invalid()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var invalidCustomer = customerBuilder.WithInvalidEmail().Generate();

            var customerValidator = new CustomerValidator();

            // Act
            var validationResult = await customerValidator.ValidateAsync(invalidCustomer);

            // Assert
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
