using DddExample.Domain.CustomerContext.Validators;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Validators
{
    public class CpfValidatorTests : UnitTestBase
    {

        [Test]
        public async Task Validation_should_success_when_all_properties_are_valid()
        {
            // Arrange
            var validCpf = new Cpf("70255523050");
            var cpfValidator = new CpfValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(validCpf);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Validation_should_fail_when_Document_is_empty(string invalidCpfText)
        {
            // Arrange
            var invalidCpf = new Cpf(invalidCpfText);
            var cpfValidator = new CpfValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(invalidCpf);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "CPF should not be empty");
        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("1234567890")]
        [TestCase("123456789101112")]
        public async Task Validation_should_fail_when_Document_length_is_different_from_11(string invalidCpfText)
        {
            // Arrange
            var invalidCpf = new Cpf(invalidCpfText);
            var cpfValidator = new CpfValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(invalidCpf);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "CPF must have 11 positions");
        }

        [TestCase("1234567891A")]
        [TestCase("ABCdefghijk")]
        [TestCase("1234567890`")]
        [TestCase("1234567890-")]
        [TestCase("1234567890_")]
        [TestCase("12345  67890")]
        public async Task Validation_should_fail_when_Document_has_any_non_numeric_characters(string invalidCpfText)
        {
            // Arrange
            var invalidCpf = new Cpf(invalidCpfText);
            var cpfValidator = new CpfValidator();

            // Act
            var validationResult = await cpfValidator.ValidateAsync(invalidCpf);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(e => e.ErrorMessage == "CPF must have only numbers");
        }
    }
}
