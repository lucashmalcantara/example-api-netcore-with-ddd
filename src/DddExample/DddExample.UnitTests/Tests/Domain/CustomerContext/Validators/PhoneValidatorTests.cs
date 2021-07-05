using DddExample.Domain.CustomerContext.Validators;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Validators
{
    public class PhoneValidatorTests : UnitTestBase
    {
        [Test]
        public async Task Validation_should_success_when_all_properties_are_valid()
        {
            // Arrange
            var validPhone = new Phone(31, "999999999");
            var phonneValidator = new PhoneValidator();

            // Act
            var validationResult = await phonneValidator.ValidateAsync(validPhone);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(99)]
        public async Task Validation_should_fail_when_AreaCode_is_out_of_range_11_to_98(int invalidAreaCode)
        {
            // Arrange
            var invalidPhone = new Phone(invalidAreaCode, "999999999");
            var phoneValidator = new PhoneValidator();

            // Act
            var validationResult = await phoneValidator.ValidateAsync(invalidPhone);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Phone area code should be between 11 and 98");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Validation_should_fail_when_Number_is_empty(string invalidNumber)
        {
            // Arrange
            var invalidPhone = new Phone(31, invalidNumber);
            var phoneValidator = new PhoneValidator();

            // Act
            var validationResult = await phoneValidator.ValidateAsync(invalidPhone);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Phone number should not be null or empty");
        }

        [TestCase("")]
        [TestCase("9999999")]
        public async Task Validation_should_fail_when_Number_has_less_than_8_characters(string invalidNumber)
        {
            // Arrange
            var invalidPhone = new Phone(31, invalidNumber);
            var phoneValidator = new PhoneValidator();

            // Act
            var validationResult = await phoneValidator.ValidateAsync(invalidPhone);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Phone number must have at least 8 characters");
        }

        [Test]
        public async Task Validation_should_fail_when_Number_has_more_than_9_characters()
        {
            // Arrange
            const string numberWithMoreThan9Characters = "9999999999";
            var invalidPhone = new Phone(31, numberWithMoreThan9Characters);
            var phoneValidator = new PhoneValidator();

            // Act
            var validationResult = await phoneValidator.ValidateAsync(invalidPhone);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Phone number must have a maximum of 9 characters");
        }

        [TestCase("1234567891A")]
        [TestCase("ABCdefghijk")]
        [TestCase("1234567890`")]
        [TestCase("1234567890-")]
        [TestCase("1234567890_")]
        [TestCase("12345  67890")]
        public async Task Validation_should_fail_when_Number_has_any_non_numeric_characters(string invalidNumber)
        {
            // Arrange
            var invalidPhone = new Phone(31, invalidNumber);
            var phoneValidator = new PhoneValidator();

            // Act
            var validationResult = await phoneValidator.ValidateAsync(invalidPhone);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Phone number must have only numbers");
        }
    }
}
