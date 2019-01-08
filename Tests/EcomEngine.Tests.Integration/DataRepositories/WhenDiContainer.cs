using Shouldly;
using System;
using EcomEngine.Tests.Integration.BaseClasses;
using EcomEngine.Tests.Integration.ClassData;
using Xunit;

namespace EcomEngine.Tests.Integration.DataRepositories
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(RepositoryClassData))]
        public void Repository_ShouldBeDiscoverable(Type type)
        {
			classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }
    }
}
