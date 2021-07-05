using DddExample.Api.V1.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DddExample.Api.ServicesCollections
{
    public static class MapperCollection
    {
        public static IServiceCollection AddMapperDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(new Type[] { typeof(CustomerMapProfile) });
            return services;
        }
    }
}