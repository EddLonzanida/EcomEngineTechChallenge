using System;
using System.Collections.Generic;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Data.Migrations.Data;
using Eml.Contracts.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngineTechChallenge.Tests.Unit.BaseClasses
{
    public abstract class ControllerTestBase<T, TEntity> : IDisposable
        where T : Controller
        where TEntity : class, IEntityBase<Guid>, ISearchableName
    {
        private const string SAMPLE_DATA_SOURCES = @"SampleDataSources";

        protected T controller;

        protected readonly List<EmailTemplate> emailTemplatesStub;

        protected ControllerTestBase()
        {
            emailTemplatesStub = EmailTemplateData.GetEmailTemplatesFromJson(SAMPLE_DATA_SOURCES);
        }

        public void Dispose()
        {
            controller?.Dispose();
        }
    }
}

