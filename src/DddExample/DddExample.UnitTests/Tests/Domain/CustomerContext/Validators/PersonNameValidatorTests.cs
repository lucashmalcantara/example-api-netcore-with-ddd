using DddExample.Domain.CustomerContext.Validators;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Validators
{
    public class PersonNameValidatorTests : UnitTestBase
    {
        [Test]
        public async Task Validation_should_success_when_all_properties_are_valid()
        {
            // Arrange
            var validPersonName = new PersonName("Lucas", "Alcântara");
            var personNameValidator = new PersonNameValidator();

            // Act
            var validationResult = await personNameValidator.ValidateAsync(validPersonName);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Validation_should_fail_when_FirstName_is_empty(string invalidNameText)
        {
            // Arrange
            var invalidPersonName = new PersonName(invalidNameText, "Alcântara");
            var personNameValidator = new PersonNameValidator();

            // Act
            var validationResult = await personNameValidator.ValidateAsync(invalidPersonName);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "First name should not be null or empty");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Validation_should_fail_when_LastName_is_empty(string invalidNameText)
        {
            // Arrange
            var invalidPersonName = new PersonName("Lucas", invalidNameText);
            var personNameValidator = new PersonNameValidator();

            // Act
            var validationResult = await personNameValidator.ValidateAsync(invalidPersonName);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Last name should not be null or empty");
        }

        [TestCase("")]
        [TestCase("L")]
        public async Task Validation_should_fail_when_FirstName_length_is_less_than_2(string invalidNameText)
        {
            // Arrange
            var invalidPersonName = new PersonName(invalidNameText, "Alcântara");
            var personNameValidator = new PersonNameValidator();

            // Act
            var validationResult = await personNameValidator.ValidateAsync(invalidPersonName);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "First name must be at least 2 characters long");
        }

        [TestCase("")]
        [TestCase("A")]
        public async Task Validation_should_fail_when_LastName_length_is_less_than_2(string invalidNameText)
        {
            // Arrange
            var invalidPersonName = new PersonName("Lucas", invalidNameText);
            var personNameValidator = new PersonNameValidator();

            // Act
            var validationResult = await personNameValidator.ValidateAsync(invalidPersonName);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "Last name must be at least 2 characters long");
        }
    }
}
