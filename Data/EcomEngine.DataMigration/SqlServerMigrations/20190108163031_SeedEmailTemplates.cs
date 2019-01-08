using EcomEngine.DataMigration.Seeders;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace EcomEngine.DataMigration.SqlServerMigrations
{
    public partial class SeedEmailTemplates : Migration
    {
        private const string RELATIVE_FOLDER = "SampleDataSources";

        private readonly string tableName;

        public SeedEmailTemplates()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var db = new EcomEngineDb())
            {
                EmailTemplateSeeder.Seed(db, RELATIVE_FOLDER);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"DeleteData: {tableName}");

            migrationBuilder.Sql($@"DELETE FROM {tableName}");
        }
    }
}
