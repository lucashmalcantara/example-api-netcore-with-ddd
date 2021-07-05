using DddExample.Domain.CustomerContext.Validators;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Validators
{
    public class EmailValidatorTests : UnitTestBase
    {

        [Test]
        public async Task Validation_should_success_when_all_properties_are_valid()
        {
            // Arrange
            var validEmail = new Email("lucas.alcantara@test.com");
            var emailValidator = new EmailValidator();

            // Act
            var validationResult = await emailValidator.ValidateAsync(validEmail);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Validation_should_fail_when_Address_is_empty(string invalidEmailText)
        {
            // Arrange
            var invalidEmail = new Email(invalidEmailText);
            var cpfValidator = new EmailValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(invalidEmail);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "The email should be in standard format");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("@test.com")]
        [TestCase(" @test.com")]
        [TestCase("lucas.alcantara")]
        [TestCase("lucas.alcantara@test")]
        [TestCase("lucas.alcantara@.com")]
        [TestCase("lucas.alcantara.com")]
        public async Task Validation_should_fail_when_Address_is_not_in_the_standard_format(string invalidEmailText)
        {
            // Arrange
            var invalidEmail = new Email(invalidEmailText);
            var cpfValidator = new EmailValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(invalidEmail);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "The email should be in standard format");
        }
    }
}
