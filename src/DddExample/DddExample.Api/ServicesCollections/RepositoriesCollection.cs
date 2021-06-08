using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Infrastructure.Repositories.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Api.ServicesCollections
{
    public static class RepositoriesCollection
    {
        public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
