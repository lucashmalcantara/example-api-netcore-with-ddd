using DddExample.Api.Constants;
using DddExample.IntegrationTests.Base;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api
{
    public class ApplicationBuilders : ControllerTestBase
    {
        [Test]
        public async Task Swagger_should_be_started()
        {
            // Arrange
            JObject root;

            // Act
            using (HttpClient)
            {
                var result = await HttpClient.GetStringAsync($"{ApplicationConstants.ApplicationPathBase}/swagger/v1/swagger.json");
                root = JObject.Parse(result);
            }

            // Assert
            root.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
