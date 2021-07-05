using DddExample.Domain.Core.Results;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace DddExample.UnitTests.Tests.Domain.Core.Results
{
    public class ResultTests : UnitTestBase
    {
        [Test]
        public void Ok_should_create_a_Result_with_no_error()
        {
            // Arrange -  Act
            var result = Result<string>.Ok("Some value");

            // Assert
            result.Data.Should().Be("Some value");
            result.HasError.Should().BeFalse();
            result.Errors.Should().BeEmpty();
        }

        [Test]
        public void Error_should_set_a_list_of_Error()
        {
            // Arrange
            var expectedErrorList = new List<Error> {
                new Error("SomeProperty1", "Some message 1"),
                new Error("SomeProperty2", "Some message 2")
            };

            // Act
            var result = Result<string>.Error(expectedErrorList);

            // Assert
            result.Data.Should().BeNull();
            result.HasError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expectedErrorList);
        }

        [Test]
        public void Error_should_set_an_Error_correctly()
        {
            // Arrange
            var expectedError = new Error("SomeProperty", "Some message");
            var expectedErrorList = new List<Error> { expectedError };

            // Act
            var result = Result<string>.Error(expectedError);

            // Assert
            result.Data.Should().BeNull();
            result.HasError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expectedErrorList);
        }
    }
}
