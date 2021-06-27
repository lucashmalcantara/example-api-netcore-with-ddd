using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Services;
using DddExample.Domain.CustomerContext.Validators;
using DddExample.Services.CustomerContext;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Api.ServicesCollections
{
    public static class DomainServicesCollection
    {
        public static IServiceCollection AddDomainServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Customer>, CustomerValidator>()
                .AddTransient<ICustomerService, CustomerService>();
            return services;
        }
    }
}
