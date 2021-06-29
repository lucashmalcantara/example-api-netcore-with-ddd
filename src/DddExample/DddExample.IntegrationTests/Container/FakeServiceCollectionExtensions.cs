using Microsoft.Extensions.DependencyInjection;
using System;

namespace DddExample.IntegrationTests.Container
{
    public static class FakeServiceCollectionExtensions
    {
        public static void AddTransientWithFaker<T>(this IServiceCollection services, Action<T> configure = null) where T : class
        {
            services.AddTransient(sp =>
            {
                return ConfigureFaker(configure);
            });
        }

        public static void AddScopedWithFaker<T>(this IServiceCollection services, Action<T> configure = null) where T : class
        {
            services.AddScoped(sp =>
            {
                return ConfigureFaker(configure);
            });
        }

        public static void AddSingletonWithFaker<T>(this IServiceCollection services, Action<T> configure = null) where T : class
        {
            services.AddSingleton(sp =>
            {
                return ConfigureFaker(configure);
            });
        }

        private static T ConfigureFaker<T>(Action<T> configure = null) where T : class
        {
            var fake = FakeServiceProvider.Instance.Get<T>();

            if (fake != null)
                return fake;

            fake = FakeServiceProvider.Instance.Add<T>();
            configure?.Invoke(fake);

            return fake;
        }
    }
}
