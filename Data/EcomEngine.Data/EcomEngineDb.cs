using Eml.ConfigParser.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EcomEngine.Business.Common.Entities.EcomEngineDb;
using EcomEngine.Infrastructure;
using EcomEngine.Infrastructure.Configurations;

namespace EcomEngine.Data
{
    public class EcomEngineDb : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = ConnectionStrings.EcomEngineDb;
           
			if (string.IsNullOrWhiteSpace(connString))
            {
                var configuration = ConfigBuilder.GetConfiguration(Constants.CurrentEnvironment)
                    .AddJsonFile("appsettings.json")
                    .Build();

                using (var config = new EcomEngineConnectionStringParser(configuration))
                {
                    connString = config.Value;
                }
            }
			
			optionsBuilder.UseSqlServer(connString);
        }
    }
}
//Add-Migration InitialCreate -OutputDir EcomEngineDbMigrations -Context EcomEngineDb
//Update-Database -verbose -Context EcomEngineDb
//Update-Database LastGoodMigration -verbose -Context EcomEngineDb  // Revert a migration. Note: Migrations onwards will be deleted except LastGoodMigration

//Using console or terminal:
//navigate to EcomEngine root folder
//dotnet ef migrations add InitialCreate -o EcomEngineDbMigrations -c EcomEngineDb -p Data/EcomEngine.DataMigration -s Hosts/EcomEngine.Api -v
//dotnet ef migrations add InitialStoredProcedures -o EcomEngineDbMigrations -c EcomEngineDb -p Data/EcomEngine.DataMigration -s Hosts/EcomEngine.Api -v
//dotnet ef database update -c EcomEngineDb -p Data/EcomEngine.DataMigration -s Hosts/EcomEngine.Api -v
