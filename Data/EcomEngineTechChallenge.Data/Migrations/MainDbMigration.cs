using System;
using System.Composition;
using EcomEngineTechChallenge.Data.Migrations.Data;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace EcomEngineTechChallenge.Data.Migrations
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigration : MigratorBase<EcomEngineTechChallengeDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

        private const bool ALLOW_IDENTITYINSERT_WHEN_SEEDING = true;
        
		[ImportingConstructor]
        public MainDbMigration(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString.Value, ALLOW_IDENTITYINSERT_WHEN_SEEDING)
        {
        }

        protected override void Seed(EcomEngineTechChallengeDb context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            Console.WriteLine($"Creating {dbName}..");
            context.Database.EnsureCreated();

            Console.WriteLine("Running Migration..");
            context.Database.Migrate();

            Console.WriteLine("Seeding Data..");
            EmailTemplateData.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}
	
