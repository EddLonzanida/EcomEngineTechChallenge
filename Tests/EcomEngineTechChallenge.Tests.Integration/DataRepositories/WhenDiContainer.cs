using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Tests.Integration.BaseClasses;
using Eml.DataRepository.Contracts;
using Shouldly;
using Xunit;

namespace EcomEngineTechChallenge.Tests.Integration.DataRepositories
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void RaceRepository_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IDataRepositoryGuid<EmailTemplate>>();

            exported.ShouldNotBeNull();
            exported.PageSize.ShouldBe(10);
        }
    }
}

