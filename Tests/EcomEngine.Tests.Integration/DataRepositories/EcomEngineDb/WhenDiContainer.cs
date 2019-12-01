using Shouldly;
using System;
using EcomEngine.Tests.Integration.BaseClasses;
using EcomEngine.Tests.Utils.ClassData.Conventions.EcomEngineDb;
using Xunit;

namespace EcomEngine.Tests.Integration.DataRepositories.EcomEngineDb
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(RepositoryGuidClassData))]
        public void EcomEngineRepositoryGuid_ShouldBeDiscoverable(Type type)
        {
			classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }
    }
}
