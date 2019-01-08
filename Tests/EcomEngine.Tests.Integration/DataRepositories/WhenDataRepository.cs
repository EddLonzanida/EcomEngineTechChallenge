using EcomEngine.Business.Common.Entities;
using EcomEngine.Data.Contracts;
using EcomEngine.Tests.Integration.BaseClasses;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EcomEngine.Tests.Integration.DataRepositories
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
