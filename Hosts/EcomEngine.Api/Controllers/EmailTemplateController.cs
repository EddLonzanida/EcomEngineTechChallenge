using EcomEngine.Api.Controllers.BaseClasses.EcomEngineDb;
using EcomEngine.Business.Common.Dto.EcomEngineDb;
using EcomEngine.Business.Common.Dto.EcomEngineDb.EntityHelpers;
using EcomEngine.Business.Common.Dto.EcomEngineDb.SortEnums;
using EcomEngine.Business.Common.Entities.EcomEngineDb;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using Eml.Contracts.Responses;
using Eml.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EcomEngine.Api.Controllers
{
    [Export]
    public class EmailTemplateController : CrudControllerApiWithParentSoftDeletableGuidBase<EmailTemplate
        , EmailTemplateIndexRequest
        , EmailTemplateIndexResponse
        , EmailTemplateEditCreateRequest
        , EmailTemplateDetailsCreateResponse
        , IEcomEngineDataRepositorySoftDeleteGuid<EmailTemplate>>
    {
        [ImportingConstructor]
        public EmailTemplateController(IEcomEngineDataRepositorySoftDeleteGuid<EmailTemplate> repository)
            : base(repository)
        {
        }

        #region CRUD
        [HttpGet]
        public override async Task<ActionResult<EmailTemplateIndexResponse>> Index([FromQuery]EmailTemplateIndexRequest request)
        {
            return await DoIndexAsync(request);
        }

        [HttpGet("{parentId}/Index")]
        public override async Task<ActionResult<EmailTemplateIndexResponse>> IndexWithParent([FromRoute]Guid parentId, EmailTemplateIndexRequest request)
        {
            return await DoIndexAsync(parentId, request);
        }

        [HttpGet("Suggestions")]
        public override async Task<ActionResult<List<string>>> Suggestions(string search = "")
        {
            return await DoSuggestionsAsync(search);
        }

        [HttpGet("{parentId}/Suggestions")]
        public override async Task<ActionResult<List<string>>> SuggestionsWithParent([FromRoute]Guid parentId, string search = "")
        {
            return await DoSuggestionsAsync(parentId, search);
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<EmailTemplateDetailsCreateResponse>> Details([FromRoute]Guid id)
        {
            return await DoDetailsAsync(id);
        }

        [HttpPost]
        public override async Task<ActionResult<EmailTemplateDetailsCreateResponse>> Create([FromBody]EmailTemplateEditCreateRequest request)
        {
            return await DoCreateAsync(request);
        }

        [HttpPut]
        public override async Task<ActionResult> Edit([FromBody]EmailTemplateEditCreateRequest request)
        {
            return await DoEditAsync(request);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete([FromRoute]Guid id, [FromBody]string reason)
        {
            return await DoDeleteAsync(id, reason);
        }
        #endregion // CRUD

        #region CRUD HELPERS
        protected override async Task<EmailTemplateDetailsCreateResponse> EditItemAsync(EmailTemplateEditCreateRequest request)
        {
            var entity = request.ToEntity();

            await repository.UpdateAsync(entity);

            return entity.ToDto();
        }

        protected override async Task<EmailTemplateDetailsCreateResponse> AddItemAsync(EmailTemplateEditCreateRequest request)
        {
            var entity = request.ToEntity();

            entity.Id = Guid.NewGuid();

            var newEntity = await repository.AddAsync(entity);

            return newEntity.ToDto();
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string search = "")
        {
            search = string.IsNullOrWhiteSpace(search) ? string.Empty : search;

            return await repository
                .GetAutoCompleteIntellisenseAsync(r => search == "" || r.EmailLabel.Contains(search)
                    , r => r.EmailLabel);
        }

        protected override async Task<List<string>> GetSuggestionsAsync(Guid parentId, string search = "")
        {
            search = string.IsNullOrWhiteSpace(search) ? string.Empty : search;

            return await repository
                .GetAutoCompleteIntellisenseAsync(r => r.ParentId.Equals(parentId)
                                                       && (search == "" || r.EmailLabel.Contains(search))
                    , r => r.EmailLabel);
        }

        protected override async Task<EmailTemplateIndexResponse> GetItemsAsync(EmailTemplateIndexRequest request)
        {
            var search = request.Search;

            Expression<Func<EmailTemplate, bool>> whereClause = r => search == null
                                                               || search == ""
                                                               || r.EmailLabel.Contains(search);

            var items = await GetItemsAsync(request, whereClause);

            return new EmailTemplateIndexResponse(items.Items, items.RecordCount, items.RowsPerPage);
        }

        protected override async Task<EmailTemplateIndexResponse> GetItemsAsync(Guid parentId, EmailTemplateIndexRequest request)
        {
            var search = request.Search;

            Expression<Func<EmailTemplate, bool>> whereClause = r => search == null
                                                               || search == ""
                                                               || r.EmailLabel.Contains(search);
            whereClause = whereClause.And(r => r.ParentId == parentId);

            var items = await GetItemsAsync(request, whereClause);

            return new EmailTemplateIndexResponse(items.Items, items.RecordCount, items.RowsPerPage);
        }

        protected async Task<SearchResponse<EmailTemplate>> GetItemsAsync(EmailTemplateIndexRequest request, Expression<Func<EmailTemplate, bool>> whereClause)
        {
            var orderBy = GetOrderBy(request.SortColumn, request.IsDescending);
            var result = await repository.GetPagedListAsync(request.Page, whereClause, orderBy);
            var response = new SearchResponse<EmailTemplate>(result.ToList(), result.TotalItemCount, result.PageSize);

            return response;
        }

        protected Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>> GetOrderBy(string sortColumn, bool isDesc)
        {
            Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>> orderBy;

            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                sortColumn = "Name"; //Default sort column
            }

            var eSortColumn = (eEmailTemplate)Enum.Parse(typeof(eEmailTemplate), sortColumn, true);

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eEmailTemplate.Name:

                        orderBy = r => r.OrderByDescending(x => x.EmailLabel);
                        break;

                    case eEmailTemplate.FromAddress:

                        orderBy = r => r.OrderByDescending(x => x.FromAddress);
                        break;

                    case eEmailTemplate.DateUpdated:

                        orderBy = r => r.OrderByDescending(x => x.DateUpdated);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"sortColumn: [{sortColumn}] is not supported.");
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eEmailTemplate.Name:

                    orderBy = r => r.OrderBy(x => x.EmailLabel);
                    break;

                case eEmailTemplate.FromAddress:

                    orderBy = r => r.OrderBy(x => x.FromAddress);
                    break;

                case eEmailTemplate.DateUpdated:

                    orderBy = r => r.OrderBy(x => x.DateUpdated);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"sortColumn: [{sortColumn}] is not supported.");
            }

            return orderBy;
        }

        protected override async Task<EmailTemplateDetailsCreateResponse> GetItemAsync(Guid id)
        {
            var item = await repository.GetAsync(id);

            return item?.ToDto();
        }
        #endregion // CRUD HELPERS
    }
}
