using AutoBogus;
using DddExample.IntegrationTests.Container;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;

namespace DddExample.IntegrationTests.Base
{
    public class ControllerTestBase
    {
        protected HttpClient HttpClient { get; private set; }

        [OneTimeSetUp]
        public void BaseApiTestsOneTimeSetup()
        {
            const int depth = 1;
            const string locale = "pt_BR";
            AutoFaker.Configure(config => config.WithRecursiveDepth(depth).WithLocale(locale));

            HttpClient = RestApiTestEnviroment.Instance.TestServerApi.GetTestClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            HttpClient = null;
        }
    }
}
