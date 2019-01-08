using EcomEngine.Api.Controllers.BaseClasses;
using EcomEngine.Business.Common.Entities;
using EcomEngine.Contracts.Infrastructure;
using EcomEngine.Data.Contracts;
using Eml.Contracts.Response;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EcomEngine.Api.Controllers
{
    [Export]
    public class EmailTemplateController : CrudControllerApiBase<EmailTemplate, IndexRequest>
    {
        [ImportingConstructor]
        public EmailTemplateController(IMediator mediator, IDataRepositorySoftDeleteGuid<EmailTemplate> repository)
            : base(mediator, repository)
        {
        }

        [HttpGet]
        [Produces(typeof(SearchResponse<EmailTemplate>))]
        public override async Task<IActionResult> Index(int? page = 1, bool? desc = false, int? sortColumn = 0, string search = "")
        {
            var request = new IndexRequest(page, desc, sortColumn, search);
            var response = await DoIndexAsync(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmailTemplate))]
        public override async Task<IActionResult> Details([FromRoute]Guid id)
        {
            return await DoDetailsAsync(id);
        }

        [HttpGet("Suggestions")]
        [Produces(typeof(string[]))]
        public override async Task<IActionResult> Suggestions(string search = "")
        {
            return await DoSuggestionsAsync(search);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Edit([FromRoute]Guid id, [FromBody]EmailTemplate item)
        {
            return await DoEditAsync(id, item);
        }

        [HttpGet("Create")]
        [HttpPost]
        [Produces(typeof(EmailTemplate))]
        public override async Task<IActionResult> Create([FromBody]EmailTemplate item)
        {
            return await DoCreateAsync(item);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete([FromRoute]Guid id, string reason = "")
        {
            return await DoDeleteAsync(id, reason);
        }

        protected override async Task<ISearchResponse<EmailTemplate>> GetItemsAsync(IndexRequest request)
        {
            var search = request.Search;

            Expression<Func<EmailTemplate, bool>> whereClause = r => search == null
                                                               || search == ""
                                                               || r.SearchableName.Contains(search, StringComparison.CurrentCultureIgnoreCase);

            var orderBy = GetOrderBy(request.SortColumn, request.IsDescending);
            var result = await repository.GetPagedListAsync(request.Page, whereClause, orderBy);
            var response = new SearchResponse<EmailTemplate>(result.ToList(), result.TotalItemCount, result.PageSize);

            return response;
        }

        protected override Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<EmailTemplate>, IOrderedQueryable<EmailTemplate>> orderBy = null;

            var eSortColumn = (eEmailTemplate)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eEmailTemplate.EmailLabel:

                        orderBy = r => r.OrderByDescending(x => x.EmailLabel);
                        break;

                    case eEmailTemplate.FromAddress:

                        orderBy = r => r.OrderByDescending(x => x.FromAddress);
                        break;

                    case eEmailTemplate.DateUpdated:

                        orderBy = r => r.OrderByDescending(x => x.DateUpdated);
                        break;


                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eEmailTemplate.EmailLabel:

                    orderBy = r => r.OrderBy(x => x.EmailLabel);
                    break;

                case eEmailTemplate.FromAddress:

                    orderBy = r => r.OrderBy(x => x.FromAddress);
                    break;

                case eEmailTemplate.DateUpdated:

                    orderBy = r => r.OrderBy(x => x.DateUpdated);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }
    }
}
