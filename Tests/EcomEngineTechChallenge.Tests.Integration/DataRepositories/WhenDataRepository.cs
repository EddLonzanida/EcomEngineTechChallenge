using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Tests.Integration.BaseClasses;
using Eml.DataRepository.Contracts;

namespace EcomEngineTechChallenge.Tests.Integration.DataRepositories
{
    public class WhenDataRepository : IntegrationTestDbBase
    {
        [Fact]
        private async Task Get_ShouldReturnEmailTemplates()
        {
            const string searchTerm = "Email Template 29";
            var repository = classFactory.GetExport<IDataRepositoryGuid<EmailTemplate>>();

            var results = await repository.GetPagedListAsync(1, r => searchTerm == "" || r.EmailLabel.ToLower().Contains(searchTerm),
                r => r.OrderBy(s => s.SearchableName));
          
            results.TotalItemCount.ShouldBe(2);
        }
    }
}


