using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcomEngineTechChallenge.ApiHost.Controllers;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Tests.Unit.BaseClasses;
using Eml.DataRepository.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using X.PagedList;
using Xunit;

namespace EcomEngineTechChallenge.Tests.Unit.Controllers
{
    public class EmailTemplateControllerTests : ControllerTestBase<EmailTemplateController, EmailTemplate>
    {
        private readonly IDataRepositoryGuid<EmailTemplate> repository;

        public EmailTemplateControllerTests()
        {
            repository = Substitute.For<IDataRepositoryGuid<EmailTemplate>>();
            controller = new EmailTemplateController(repository);
        }

        [Fact]
        public async Task Controller_ShouldGetEmailTemplates()
        {
            var pagedList = new PagedList<EmailTemplate>(emailTemplatesStub, 1, 10);
            repository.GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>>>())
                .Returns(pagedList);

            var response = await controller.Index() as OkObjectResult; ;

            await repository.Received()
                .GetPagedListAsync(Arg.Any<int>(),
                    Arg.Any<Expression<Func<EmailTemplate, bool>>>(),
                    Arg.Any<Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>>>());
            var result = response?.Value as IEnumerable<EmailTemplate>;
            result?.ToList().Count.ShouldBe(10);
        }
    }
}
