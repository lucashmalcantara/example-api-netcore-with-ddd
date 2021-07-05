using DddExample.Infrastructure.Logging;
using DddExample.Infrastructure.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Api.ServicesCollections
{
    public static class LoggerCollection
    {
        public static IServiceCollection AddCustomLoggerDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBasicLogger<>), typeof(BasicLogger<>));

            return services;
        }
    }
}
