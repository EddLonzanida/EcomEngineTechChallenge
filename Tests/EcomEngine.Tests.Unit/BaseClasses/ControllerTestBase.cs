using EcomEngine.Business.Common.Entities.EcomEngineDb;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace EcomEngine.Tests.Unit.BaseClasses
{
    [Collection(ControllerTestBaseFixture.COLLECTION_DEFINITION)]
    public abstract class ControllerTestBase<TController> : IDisposable
        where TController : ControllerBase
    {
        protected TController controller;

        protected readonly List<EmailTemplate> emailTemplatesStub;

        protected ControllerTestBase()
        {
            emailTemplatesStub = ControllerTestBaseFixture.EmailTemplatesStub;
        }

        public void Dispose()
        {
        }
    }
}
