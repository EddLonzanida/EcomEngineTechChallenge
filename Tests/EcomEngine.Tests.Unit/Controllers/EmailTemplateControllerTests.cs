using EcomEngine.Api.Controllers;
using EcomEngine.Business.Common.Entities.EcomEngineDb;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using EcomEngine.Tests.Unit.BaseClasses;
using EcomEngine.Tests.Utils.Extensions;
using NSubstitute;
using Shouldly;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using Xunit;

namespace EcomEngine.Tests.Unit.Controllers
{
    public class EmailTemplateControllerTests : ControllerTestBase<EmailTemplateController>
    {
        private readonly IEcomEngineDataRepositorySoftDeleteGuid<EmailTemplate> repository;

        public EmailTemplateControllerTests()
        {
            repository = Substitute.For<IEcomEngineDataRepositorySoftDeleteGuid<EmailTemplate>>();
            controller = new EmailTemplateController(repository);
        }

        [Fact]
        public async Task Controller_ShouldGetEmailTemplates()
        {
            var pagedList = new PagedList<EmailTemplate>(emailTemplatesStub, 1, 10);
            repository.GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>>>())
                .Returns(pagedList);

            var response = await controller.Index(null); ;

            await repository.Received()
                .GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>>>());
            var result = response.GetValue();
            result.Items.Count.ShouldBe(10);
        }
    }
}
