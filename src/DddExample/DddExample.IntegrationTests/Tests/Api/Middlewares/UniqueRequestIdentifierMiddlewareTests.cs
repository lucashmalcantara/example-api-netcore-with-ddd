using DddExample.Api.Middlewares;
using DddExample.Infrastructure.Logging.Interfaces;
using DddExample.IntegrationTests.Base;
using DddExample.IntegrationTests.Container;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api.Middlewares
{
    public class UniqueRequestIdentifierMiddlewareTests : ControllerTestBase
    {
        [Test]
        public async Task Invoke_should_add_the_correlationid_in_the_context()
        {
            // Arrange
            var entryPointMiddlewareLogger = FakeServiceProvider.Instance.GetOrAddIfNotExists<IBasicLogger<EntryPointMiddleware>>();

            A.CallTo(() => entryPointMiddlewareLogger.LogInformation(A<string>.Ignored, A<string>.Ignored, A<object>.Ignored))
                .DoesNothing();

            // Act
            var response = await HttpClient.GetAsync("health/liveness");
            var correlationIdValue = response.Headers.GetValues("X-Correlation-Id").First();
            bool isValidGuid = Guid.TryParse(correlationIdValue, out _);

            // Assert
            isValidGuid.Should().BeTrue();
        }
    }
}
