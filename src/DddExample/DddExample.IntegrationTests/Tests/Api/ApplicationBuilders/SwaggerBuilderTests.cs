using DddExample.Api.Constants;
using DddExample.IntegrationTests.Base;
using FluentAssertions;
using NUnit.Framework;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api.ApplicationBuilders
{
    public class SwaggerBuilderTests : ControllerTestBase
    {
        [Test]
        public async Task Swagger_should_be_started()
        {
            // Arrange
            JsonDocument root;

            // Act
            var result = await HttpClient.GetStringAsync($"{ApplicationConstants.ApplicationPathBase}/swagger/v1/swagger.json");
            root = JsonDocument.Parse(result);

            // Assert
            root.Should().NotBeNull();
            root.RootElement.GetRawText().Should().NotBeEmpty();
        }
    }
}
