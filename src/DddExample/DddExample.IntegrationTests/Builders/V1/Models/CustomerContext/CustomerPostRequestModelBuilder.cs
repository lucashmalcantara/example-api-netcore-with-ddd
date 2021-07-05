using AutoBogus;
using DddExample.Api.V1.Models.CustomerContext;

namespace DddExample.IntegrationTests.Builders.V1.Models.CustomerContext
{
    public class CustomerPostRequestModelBuilder : AutoFaker<CustomerPostRequestModel>
    {
        public CustomerPostRequestModelBuilder()
        {
            RuleFor(x => x.FirstName, faker => faker.Person.FirstName);
            RuleFor(x => x.LastName, faker => faker.Person.LastName);
            RuleFor(x => x.Cpf, faker => faker.Random.String2(length: 11, chars: "1234567890"));
            RuleFor(x => x.PhoneAreaCode, faker => faker.Random.Number(min: 8, max: 98));
            RuleFor(x => x.PhoneNumber, faker => faker.Random.String2(length: 9, chars: "1234567890"));
            RuleFor(x => x.Email, faker => faker.Person.Email);
            RuleFor(x => x.Birthdate, faker => faker.Person.DateOfBirth);
        }
    }
}
