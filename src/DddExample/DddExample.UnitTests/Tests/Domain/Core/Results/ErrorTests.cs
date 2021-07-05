using DddExample.Domain.Core.Results;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;

namespace DddExample.UnitTests.Tests.Domain.Core.Results
{
    public class ErrorTests : UnitTestBase
    {
        [Test]
        public void PropertyName_should_be_set_correctly()
        {
            // Arrange - Act
            var error = new Error("SomeProperty", "Some message");

            // Assert
            error.PropertyName.Should().Be("SomeProperty");
        }

        [Test]
        public void Message_should_be_set_correctly()
        {
            // Arrange - Act
            var error = new Error("SomeProperty", "Some message");

            // Assert
            error.Message.Should().Be("Some message");
        }

        [Test]
        public void PropertyName_should_be_null_when_the_object_was_contructed_using_one_parameter_constructor()
        {
            // Arrange - Act
            var error = new Error("Some message");

            // Assert
            error.PropertyName.Should().BeNull();
        }

        [Test]
        public void Message_should_be_set_correctly_when_the_object_was_contructed_using_one_parameter_constructor()
        {
            // Arrange - Act
            var error = new Error("Some message");

            // Assert
            error.Message.Should().Be("Some message");
        }
    }
}
