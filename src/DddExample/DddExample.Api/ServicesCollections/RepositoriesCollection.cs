using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Domain.CustomerContext.ValueObjects;
using DddExample.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DddExample.Api.ServicesCollections
{
    public static class RepositoriesCollection
    {
        public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
        {
            services.AddSingleton(c => FakeCustomerDatabase())
                .AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }

        private static IList<Customer> FakeCustomerDatabase() =>
            new List<Customer>
            {
                        new Customer(new PersonName("Lucas", "Alcântara"),
                            new Cpf("12345678900"),
                            new Phone(31,"999999999"),
                            new Email("lucas@domain.com"),
                            new DateTime(1900,8,11)),
                        new Customer(new PersonName("Mikaelly", "Silva"),
                            new Cpf("98765432100"),
                            new Phone(31,"988888888"),
                            new Email("mikaelly@domain.com"),
                            new DateTime(1900,1,1))
            };
    }
}
