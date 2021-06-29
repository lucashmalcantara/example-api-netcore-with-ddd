using DddExample.Api.Middlewares;
using DddExample.Infrastructure.Logging.Interfaces;
using DddExample.IntegrationTests.Base;
using DddExample.IntegrationTests.Container;
using FakeItEasy;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api.Middlewares
{
    public class EntryPointMiddlewareTests : ControllerTestBase
    {
        [Test]
        public async Task Invoke_should_log_any_request()
        {
            // Arrange
            var logger = FakeServiceProvider.Instance.GetOrAddIfNotExists<IBasicLogger<EntryPointMiddleware>>();

            // Act
            _ = await HttpClient.GetAsync("health/liveness");

            // Assert
            A.CallTo(() => logger.LogInformation("Invoke", A<string>.Ignored, A<object>.Ignored))
                .MustHaveHappened();
        }
    }
}
