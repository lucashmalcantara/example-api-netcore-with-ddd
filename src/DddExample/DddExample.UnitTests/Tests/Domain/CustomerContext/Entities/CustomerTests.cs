using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.UnitTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace DddExample.UnitTests.Tests.Domain.CustomerContext.Entities
{
    public class CustomerTests : UnitTestBase
    {
        [Test]
        public void Custructor_should_create_a_new_Id()
        {
            // Arrange
            var personName = new PersonName("Lucas", "Alcântara");
            var cpf = new Cpf("12345678900");
            var phone = new Phone(31, "999999999");
            var email = new Email("lucas@tests.com");
            var birthDate = new DateTime(1900, 1, 1);

            // Act
            var customer = new Customer(
                personName,
                cpf,
                phone,
                email,
                birthDate);

            // Assert
            customer.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Constructor_should_set_all_properties_correctly()
        {
            // Arrange
            var personName = new PersonName("Lucas", "Alcântara");
            var cpf = new Cpf("12345678900");
            var phone = new Phone(31, "999999999");
            var email = new Email("lucas@tests.com");
            var birthDate = new DateTime(1900, 1, 1);

            // Act
            var customer = new Customer(
                personName,
                cpf,
                phone,
                email,
                birthDate);

            // Assert
            customer.Name.Should().Be(new PersonName("Lucas", "Alcântara"));
            customer.Cpf.Should().Be(new Cpf("12345678900"));
            customer.Phone.Should().Be(new Phone(31, "999999999"));
            customer.Email.Should().Be(new Email("lucas@tests.com"));
            customer.Birthdate.Should().Be(new DateTime(1900, 1, 1));
        }
    }
}
