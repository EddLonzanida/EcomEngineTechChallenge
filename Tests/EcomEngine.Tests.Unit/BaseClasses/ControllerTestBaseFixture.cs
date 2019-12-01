using EcomEngine.Business.Common.Entities.EcomEngineDb;
using EcomEngine.DataMigration.Seeders;
using EcomEngine.Tests.Unit.Stubs;
using System;
using System.Collections.Generic;
using Xunit;

namespace EcomEngine.Tests.Unit.BaseClasses
{
    public class ControllerTestBaseFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "ControllerTestBaseFixture CollectionDefinition";

        private const string SAMPLE_DATA_SOURCES = @"SampleDataSources";

        public static RepositoryStubs RepositoryStub { get; private set; }

        public static List<EmailTemplate> EmailTemplatesStub { get; private set; }

        public ControllerTestBaseFixture()
        {
            EmailTemplatesStub = EmailTemplateSeeder.GetEmailTemplatesFromJson(SAMPLE_DATA_SOURCES);
            RepositoryStub = new RepositoryStubs();
        }

        public void Dispose()
        {
            RepositoryStub?.Dispose();
        }
    }

    [CollectionDefinition(ControllerTestBaseFixture.COLLECTION_DEFINITION)]
    public class ControllerTestBaseFixtureCollectionDefinition : ICollectionFixture<ControllerTestBaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
