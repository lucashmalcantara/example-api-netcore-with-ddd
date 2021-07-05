using DddExample.Domain.Core.Results.Extensions;
using DddExample.UnitTests.Base;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DddExample.UnitTests.Tests.Domain.Core.Results.Extensions
{
    public class ResultConverterExtensionsTests : UnitTestBase
    {
        #region Auxiliary classes to test the scenarios.
        private class TestValidator : AbstractValidator<TestObject>
        {
            public TestValidator() =>
                RuleFor(x => x.Value).NotEmpty().WithMessage("The value should not be empty");
        }

        private class TestObject
        {
            public string Value { get; private set; }

            public TestObject(string value)
            {
                Value = value;
            }
        }
        #endregion

        [Test]
        public async Task ToErrorResult_should_convert_ValidationFailure_to_SimpleResult()
        {
            // Arrange
            var testObject = new TestObject("");
            var validator = new TestValidator();
            var validationResult = await validator.ValidateAsync(testObject);

            // Act
            var domainResult = validationResult.Errors.ToErrorResult();

            // Assert
            domainResult.HasError.Should().BeTrue();
            domainResult.Errors.Count.Should().Be(1);
            domainResult.Errors.First().PropertyName.Should().Be("Value");
            domainResult.Errors.First().Message.Should().Be("The value should not be empty");
        }

        [Test]
        public async Task Typed_ToErrorResult_should_convert_ValidationFailure_to_Result()
        {
            // Arrange
            var testObject = new TestObject("");
            var validator = new TestValidator();
            var validationResult = await validator.ValidateAsync(testObject);

            // Act
            var domainResult = validationResult.Errors.ToErrorResult<TestObject>();

            // Assert
            domainResult.HasError.Should().BeTrue();
            domainResult.Errors.Count.Should().Be(1);
            domainResult.Errors.First().PropertyName.Should().Be("Value");
            domainResult.Errors.First().Message.Should().Be("The value should not be empty");
            domainResult.Data.Should().BeNull();
        }
    }
}
