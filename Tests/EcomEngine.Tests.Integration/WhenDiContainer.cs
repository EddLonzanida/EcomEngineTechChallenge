using Eml.DataRepository.Attributes;
using Eml.DataRepository.Extensions;
using EcomEngine.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace EcomEngine.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void ProductionMigrator_ShouldBeDiscoverable()
        {
            var dbMigration = classFactory.GetMigrator(Environments.PRODUCTION);

            dbMigration.ShouldNotBeNull();
        }
    }
}
