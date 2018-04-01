using EcomEngineTechChallenge.ApiHost.Controllers;
using EcomEngineTechChallenge.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace EcomEngineTechChallenge.Tests.Integration.Controllers
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void EmailTemplateControllerController_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<EmailTemplateController>();

            exported.ShouldNotBeNull();
        }
    }
}

