using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;
using EcomEngineTechChallenge.Data;
using EcomEngineTechChallenge.Data.Migrations.Seeders;

namespace EcomEngineTechChallenge.Tests.Integration.Migrations
{
    [DbMigratorExport(Environments.INTEGRATIONTEST)]
    public class IntegrationTestDbMigration : MigratorBase<EcomEngineTechChallengeDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

		[ImportingConstructor]
        public IntegrationTestDbMigration(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString.Value)
        {
        }

        protected override void Seed(EcomEngineTechChallengeDb context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            Console.WriteLine($"Deleting {dbName}..");
            context.Database.EnsureDeleted();

            Console.WriteLine($"Creating {dbName}..");
            context.Database.EnsureCreated();

            Console.WriteLine("Running Migration..");
            context.Database.Migrate();

            Console.WriteLine("Seeding Data..");
            EmailTemplateSeeder.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}

