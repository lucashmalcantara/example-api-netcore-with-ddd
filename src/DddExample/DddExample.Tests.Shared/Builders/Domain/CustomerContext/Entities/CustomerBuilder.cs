using AutoBogus;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.ValueObjects;
using System;

namespace DddExample.Tests.Shared.Builders.Domain.CustomerContext.Entities
{
    public class CustomerBuilder : AutoFaker<Customer>
    {
        public CustomerBuilder()
        {
            RuleFor(x => x.Name, faker =>
                new PersonName(faker.Person.FirstName, faker.Person.LastName));

            RuleFor(x => x.Cpf, faker =>
                new Cpf(faker.Random.String2(length: 11, chars: "1234567890")));

            RuleFor(x => x.Phone, faker =>
                new Phone(areaCode: faker.Random.Number(min: 8, max: 98),
                          number: faker.Random.String2(length: 9, chars: "1234567890")));

            RuleFor(x => x.Email, faker =>
                new Email(faker.Person.Email));

            RuleFor(x => x.Birthdate, faker => faker.Person.DateOfBirth);
        }

        public CustomerBuilder WithBirthdate(DateTime birthdate)
        {
            RuleFor(x => x.Birthdate, birthdate);
            return this;
        }

        public CustomerBuilder WithInvalidPersonName()
        {
            var invalidPersonName = new PersonName("", "");
            RuleFor(x => x.Name, invalidPersonName);
            return this;
        }

        public CustomerBuilder WithInvalidCpf()
        {
            var invalidCpf = new Cpf("");
            RuleFor(x => x.Cpf, invalidCpf);
            return this;
        }

        public CustomerBuilder WithInvalidPhone()
        {
            var invalidPhone = new Phone(-1, "");
            RuleFor(x => x.Phone, invalidPhone);
            return this;
        }

        public CustomerBuilder WithInvalidEmail()
        {
            var email = new Email("");
            RuleFor(x => x.Email, email);
            return this;
        }
    }
}
