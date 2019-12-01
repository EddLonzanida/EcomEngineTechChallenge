using EcomEngine.Api.Controllers;
using EcomEngine.Business.Common.Dto.EcomEngineDb;
using EcomEngine.Business.Common.Entities.EcomEngineDb;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using EcomEngine.Infrastructure;
using EcomEngine.Tests.Integration.BaseClasses;
using EcomEngine.Tests.Utils.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EcomEngine.Tests.Integration.Controllers
{
	public class EmailTemplateControllerTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task EmailTemplateController_ShouldGetItems()
        {
            var controller = classFactory.GetExport<EmailTemplateController>();

            var sut = await controller.Index(null);

            sut.ShouldNotBeNull();

            var response = sut.GetValue();

            response.ShouldNotBeNull();
            response.RecordCount.ShouldBeGreaterThan(0);
            response.RowsPerPage.ShouldBe(10);
        }

        [Fact]
        public async Task EmailTemplateController_ShouldGetSuggestions()
        {
            var controller = classFactory.GetExport<EmailTemplateController>();

            var sut = await controller.Suggestions(null);

            sut.ShouldNotBeNull();

            var response = sut.GetValue();

            response.ShouldNotBeNull();
            response.Count.ShouldBe(ApplicationSettings.Config.IntellisenseCount);
        }

        [Fact]
        public async Task EmailTemplateController_ShouldPerformCrudOperations()
        {
            const string EMAIL_LABEL = "IntegrationTest Name";
            const string EMAIL_LABEL_UPDATED = "IntegrationTest Name Updated";
            Guid defaultId = default;

            //ensure no remnants of failed tests 
            await EnsureNoCrudRemnants(EMAIL_LABEL);
            await EnsureNoCrudRemnants(EMAIL_LABEL_UPDATED);

            //CREATE
            var createEmailTemplateRequest = new EmailTemplateEditCreateRequest { Name = EMAIL_LABEL };
            var controller = classFactory.GetExport<EmailTemplateController>();

            var sutDetailsCreate = await controller.Create(createEmailTemplateRequest);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status201Created);

            var insertedItem = sutDetailsCreate.GetValue();
            var insertedItemId = insertedItem.Id; //store id for future use

            insertedItemId.ShouldNotBe(defaultId);

            //DETAILS
            sutDetailsCreate = await controller.Details(insertedItemId);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            insertedItem = sutDetailsCreate.GetValue();
            insertedItem.ShouldNotBeNull();
            insertedItem.EmailLabel.ShouldBe(EMAIL_LABEL);

            //EDIT
            var updateEmailTemplateRequest = new EmailTemplateEditCreateRequest
            {
                Id = insertedItemId,
                Name = EMAIL_LABEL_UPDATED,
            };

            sutDetailsCreate = await controller.Edit(updateEmailTemplateRequest);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            //SUGGESTIONS
            var sutSuggestions = await controller.Suggestions(string.Empty);

            sutSuggestions.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            var suggestions = sutSuggestions.GetValue();

            suggestions.ShouldNotBeNull();
            suggestions.Count.ShouldBeGreaterThan(0);

            //INDEX
            var sutIndex = await controller.Index(null);

            sutIndex.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            var pagedRows = sutIndex.GetValue();

            pagedRows.ShouldNotBeNull();
            pagedRows.Items.Count.ShouldBeGreaterThan(0);

            //DELETE
            var sutDelete = await controller.Delete(insertedItemId, "Integration Test");

            sutDelete.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            //Retest after Delete
            //INDEX search deleted row
            var indexRequest = new EmailTemplateIndexRequest { Search = EMAIL_LABEL_UPDATED };

            sutIndex = await controller.Index(indexRequest);

            sutIndex.GetStatusCode().ShouldBe(StatusCodes.Status200OK);

            pagedRows = sutIndex.GetValue();

            pagedRows.ShouldNotBeNull();

            var rowCountAfterDelete = pagedRows.Items.Count;

            rowCountAfterDelete.ShouldBe(0);

            // Validate the the row has been deleted
            sutDetailsCreate = await controller.Details(insertedItemId);

            sutDetailsCreate.GetStatusCode().ShouldBe(StatusCodes.Status404NotFound);

            var deletedItem = sutDetailsCreate.GetValue();

            deletedItem.ShouldBeNull();

            await EnsureNoCrudRemnants(EMAIL_LABEL);
            await EnsureNoCrudRemnants(EMAIL_LABEL_UPDATED);
        }
		
		private async Task EnsureNoCrudRemnants(string searchableText)
        {
            var repository = classFactory.GetExport<IEcomEngineDataRepositorySoftDeleteGuid<EmailTemplate>>();
            var context = await repository.GetDb();

            using (var connection = context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    var sql = $"DELETE FROM EmailTemplates WHERE [EmailLabel] LIKE '%{searchableText}%';";

                    command.CommandText = sql;

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
