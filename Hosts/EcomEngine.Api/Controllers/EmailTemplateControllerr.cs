using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcomEngine.Api.Controllers.BaseClasses;
using EcomEngine.Business.Common.Dto;
using EcomEngine.Business.Common.Entities;
using EcomEngine.Data.Contracts;
using Eml.Contracts.Response;
using Eml.ControllerBase;
using Eml.DataRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngine.Api.Controllers
{
    [Export]
    public class EmailTemplateController : CrudControllerApiBase<EmailTemplate, EmailIndexRequest>
    {
        [ImportingConstructor]
        public EmailTemplateController(IDataRepositorySoftDeleteGuid<EmailTemplate> repository)
            : base(repository)
        {
        }

        [HttpGet]
        [Produces(typeof(SearchResponse<EmailTemplate>))]
        public override async Task<IActionResult> Index(int? page = 1, bool? desc = false, int? sortColumn = 0, string search = "")
        {
            var request = new EmailIndexRequest(page, desc, sortColumn, search);
            var response = await DoIndexAsync(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Details([FromRoute]Guid id)
        {
            return await DoDetailsAsync(id);
        }

        [HttpGet("Suggestions")]
        public override async Task<IActionResult> Suggestions(string search = "")
        {
            return await DoGetSuggestionsAsync(search);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]EmailTemplate item)
        {
            return await DoPutAsync(id, item);
        }

        [HttpPost]
        public override async Task<IActionResult> Post([FromBody]EmailTemplate item)
        {
            return await DoPostAsync(item);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete([FromRoute]Guid id, string reason = "")
        {
            return await DoDeleteAsync(id, reason);
        }

        protected override async Task<ISearchResponse<EmailTemplate>> GetItemsAsync(EmailIndexRequest request)
        {
            var search = request.Search.ToLower();
            var sortColumn = request.SortColumn;
            var desc = request.IsDescending;
            var page = request.Page;
            Expression<Func<EmailTemplate, bool>> whereClause = r => search == "" || r.SearchableName.ToLower().Contains(search);
            var orderBy = GetOrderBy((eSortColumn)sortColumn, desc);
            var items = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return new SearchResponse<EmailTemplate>(items, items.TotalItemCount, repository.PageSize);
        }

        private static Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>> GetOrderBy(eSortColumn sortColumn, bool isdesc)
        {
            Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>> orderBy = r => r.OrderByDescending(x => x.SearchableName);

            if (isdesc)
            {
                switch (sortColumn)
                {
                    case eSortColumn.EmailLabel:
                        orderBy = r => r.OrderByDescending(x => x.EmailLabel);
                        break;
                    case eSortColumn.FromAddress:
                        orderBy = r => r.OrderByDescending(x => x.FromAddress);
                        break;
                    case eSortColumn.DateUpdated:
                        orderBy = r => r.OrderByDescending(x => x.DateUpdated);
                        break;
                    default:
                        break;
                }

                return orderBy;
            }

            switch (sortColumn)
            {
                case eSortColumn.EmailLabel:
                    orderBy = r => r.OrderBy(x => x.EmailLabel);
                    break;
                case eSortColumn.FromAddress:
                    orderBy = r => r.OrderBy(x => x.FromAddress);
                    break;
                case eSortColumn.DateUpdated:
                    orderBy = r => r.OrderBy(x => x.DateUpdated);
                    break;
                default:
                    orderBy = r => r.OrderBy(x => x.SearchableName);
                    break;
            }

            return orderBy;
        }
    }
}
