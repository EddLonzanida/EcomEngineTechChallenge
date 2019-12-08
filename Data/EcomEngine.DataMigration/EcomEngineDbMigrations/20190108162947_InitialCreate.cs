using System;
using EcomEngine.DataMigration.EcomEngineDbMigrations.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomEngine.DataMigration.EcomEngineDbMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    EmailTemplateId = table.Column<Guid>(nullable: true),
                    EmailLabel = table.Column<string>(nullable: true),
                    FromAddress = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    TemplateText = table.Column<string>(nullable: true),
                    EmailType = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    LoadDrafts = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    BccAddress = table.Column<string>(nullable: true),
                    VersionCount = table.Column<int>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: true),
                    DeletionReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailTemplateId",
                table: "EmailTemplates",
                column: "EmailTemplateId");

            //NLog
            migrationBuilder.Sql(NLogSql.GetCreateLogTable());
            migrationBuilder.Sql(NLogSql.GetInsertLogSp());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            //NLog
            migrationBuilder.Sql(NLogSql.GetDropSp());
            migrationBuilder.DropTable("Logs");
        }
    }
}
