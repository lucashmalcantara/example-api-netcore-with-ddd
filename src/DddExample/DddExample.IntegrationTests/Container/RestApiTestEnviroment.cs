using DddExample.Api;
using DddExample.Api.Middlewares;
using DddExample.Domain.CustomerContext.Services;
using DddExample.Infrastructure.Logging.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Container
{
    public sealed class RestApiTestEnviroment
    {
        #region Singleton Pattern
        private static readonly RestApiTestEnviroment _instance = new RestApiTestEnviroment();
        public static RestApiTestEnviroment Instance
        {
            get { return _instance; }
        }

        static RestApiTestEnviroment() { }
        private RestApiTestEnviroment() { BaseApiTestOneTimeSetup(); } 
        #endregion

        public IHost TestServerApi { get; private set; }

        public async Task BaseApiTestOneTimeSetup()
        {
            var hostBuilder = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.ConfigureTestServices(services =>
                    {
                        services.AddTransientWithFaker<IBasicLogger<EntryPointMiddleware>>();
                        services.AddTransientWithFaker<IBasicLogger<GlobalExceptionHandlerMiddleware>>();
                        services.AddTransientWithFaker<ICustomerService>();
                    });
                    webBuilder.UseStartup<Startup>();
                });

            TestServerApi = await hostBuilder.StartAsync();
        }
    }
}
