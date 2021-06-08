using DddExample.Domain.CustomerContext.Services;
using DddExample.Services.CustomerContext;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Api.ServicesCollections
{
    public static class DomainServicesCollection
    {
        public static IServiceCollection AddDomainServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            return services;
        }
    }
}
