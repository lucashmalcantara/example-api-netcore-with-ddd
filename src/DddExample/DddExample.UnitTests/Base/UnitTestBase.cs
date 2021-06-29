using AutoBogus;
using Autofac.Extras.FakeItEasy;
using NUnit.Framework;


namespace DddExample.UnitTests.Base
{
    public class UnitTestBase
    {
        protected AutoFake AutoFake { get; set; }

        [SetUp]
        public void SetUpBase()
        {
            const int depth = 1;
            const string locale = "pt_BR";
            AutoFaker.Configure(config => config.WithRecursiveDepth(depth).WithLocale(locale));
            AutoFake = new AutoFake();
        }

        [TearDown]
        public void TearDown()
        {
            AutoFake.Dispose();
        }
    }
}
