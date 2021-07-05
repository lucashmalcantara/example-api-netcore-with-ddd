using DddExample.Api.Constants;
using DddExample.Api.Middlewares;
using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Infrastructure.Logging.Interfaces;
using DddExample.IntegrationTests.Base;
using DddExample.IntegrationTests.Container;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddlewareTests : ControllerTestBase
    {
        [Test]
        public async Task Any_exception_should_be_logged_when_it_is_thrown_through_any_middleware_below_GlobalExceptionHandlerMiddleware()
        {
            // Arrange
            var entryPointMiddlewareLogger = FakeServiceProvider.Instance.GetOrAddIfNotExists<IBasicLogger<EntryPointMiddleware>>();

            A.CallTo(() => entryPointMiddlewareLogger.LogInformation(A<string>.Ignored, A<string>.Ignored, A<object>.Ignored))
                .Throws(new Exception("Some exception"));

            var globalExceptionHandlerMiddlewareLogger = FakeServiceProvider.Instance.GetOrAddIfNotExists<IBasicLogger<GlobalExceptionHandlerMiddleware>>();

            // Act
            _ = await HttpClient.GetAsync("health/liveness");

            // Assert
            A.CallTo(() => globalExceptionHandlerMiddlewareLogger
                    .LogException("Invoke", A<string>.Ignored, A<Exception>.Ignored, A<object>.Ignored))
                .MustHaveHappened();
        }

        [Test]
        public async Task Server_response_should_be_500InternalServerError_when_an_unhandled_exception_is_thrown_through_any_middleware_below_GlobalExceptionHandlerMiddleware()
        {
            // Arrange
            var entryPointMiddlewareLogger = FakeServiceProvider.Instance.Get<IBasicLogger<EntryPointMiddleware>>();

            A.CallTo(() => entryPointMiddlewareLogger.LogInformation(A<string>.Ignored, A<string>.Ignored, A<object>.Ignored))
                .Throws(new Exception("Some exception"));

            // Act
            var response = await HttpClient.GetAsync("health/liveness");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IReadOnlyCollection<Error>>(content);

            // Assert
            response.Should().Be500InternalServerError();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task Server_response_should_be_500InternalServer_when_an_unhandled_exception_is_thrown_through_any_controller()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetAllAsync())
                .Throws(new Exception("Some Exception"));

            var expectedResponse = new List<Error> { new Error("Some Exception") };

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IReadOnlyCollection<Error>>(content);

            // Assert
            response.Should().Be500InternalServerError();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task Any_exception_should_be_logged_when_it_is_thrown_through_any_controller()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetAllAsync())
                .Throws(new Exception("Some Exception"));

            var globalExceptionHandlerMiddlewareLogger = FakeServiceProvider.Instance.Get<IBasicLogger<GlobalExceptionHandlerMiddleware>>();

            // Act
            _ = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");

            // Assert
            A.CallTo(() => globalExceptionHandlerMiddlewareLogger
                .LogException("Invoke", A<string>.Ignored, A<Exception>.Ignored, A<object>.Ignored))
                .MustHaveHappened();
        }
    }
}