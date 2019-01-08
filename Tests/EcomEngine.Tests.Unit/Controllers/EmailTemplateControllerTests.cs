using EcomEngine.Api.Controllers;
using EcomEngine.Business.Common.Entities;
using EcomEngine.Data.Contracts;
using EcomEngine.Tests.Unit.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using Xunit;

namespace EcomEngineTechChallenge.Tests.Unit.Controllers
{
    public class EmailTemplateControllerTests : ControllerTestBase<EmailTemplateController, EmailTemplate>
    {
        private readonly IDataRepositorySoftDeleteGuid<EmailTemplate> repository;

        public EmailTemplateControllerTests()
        {
            repository = Substitute.For<IDataRepositorySoftDeleteGuid<EmailTemplate>>();
            controller = new EmailTemplateController(mediator, repository);
        }

        [Fact]
        public async Task Controller_ShouldGetEmailTemplates()
        {
            var pagedList = new PagedList<EmailTemplate>(emailTemplatesStub, 1, 10);
            repository.GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>>>())
                .Returns(pagedList);

            var response = await controller.Index() as OkObjectResult; ;

            await repository.Received()
                .GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>>>());
            var result = response?.Value as IEnumerable<EmailTemplate>;
            result?.ToList().Count.ShouldBe(10);
        }
    }
}
