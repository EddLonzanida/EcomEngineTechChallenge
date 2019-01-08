using EcomEngine.Business.Common.Entities;
using Eml.Contracts.Entities;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace EcomEngine.Tests.Unit.BaseClasses
{
    [Collection(ControllerTestBaseFixture.COLLECTION_DEFINITION)]
    public abstract class ControllerTestBase<TController, TEntity> : IDisposable
        where TController : Controller
        where TEntity : class, IEntityBase<Guid>, ISearchableName
    {
        private const string SAMPLE_DATA_SOURCES = @"SampleDataSources";

        protected TController controller;

        protected readonly List<EmailTemplate> emailTemplatesStub;
        protected readonly IMediator mediator;

        protected ControllerTestBase()
        {
            emailTemplatesStub = ControllerTestBaseFixture.EmailTemplatesStub;
            mediator = Substitute.For<IMediator>();
        }

        public void Dispose()
        {
            controller?.Dispose();
        }
    }
}
