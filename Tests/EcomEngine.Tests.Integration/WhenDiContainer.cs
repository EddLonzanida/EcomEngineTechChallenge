using Eml.DataRepository.Extensions;
using EcomEngine.Infrastructure;
using EcomEngine.Tests.Integration.BaseClasses;
using EcomEngine.Tests.Utils.ClassData.Conventions;
using EcomEngine.Tests.Utils.ClassData.Conventions.EcomEngineDb;
using Shouldly;

using System;
using Xunit;

namespace EcomEngine.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void ProductionMigrator_ShouldBeDiscoverable()
        {
            var dbMigration = classFactory.GetMigrator(DbNames.EcomEngine);

            dbMigration.ShouldNotBeNull();
        }
		
        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldBeExportable(Type type)
        {
            classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }

        [Theory]
        [ClassData(typeof(RepositoryGuidClassData))]
        public void RepositoryGuid_ShouldBeDiscoverable(Type type)
        {
			classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }
    }
}
