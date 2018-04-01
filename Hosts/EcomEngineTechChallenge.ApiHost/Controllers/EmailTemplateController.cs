using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcomEngineTechChallenge.ApiHost.BaseClasses;
using EcomEngineTechChallenge.Business.Common.Dto;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Contracts;
using Eml.DataRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngineTechChallenge.ApiHost.Controllers
{
    [Export]
    public class EmailTemplateController : ApiControllerBase
    {
        protected readonly IDataRepositoryGuid<EmailTemplate> repository;

        [ImportingConstructor]
        public EmailTemplateController(IDataRepositoryGuid<EmailTemplate> repository)
        {
            this.repository = repository;
        }

        [Route("")]
        [HttpGet]
        public virtual async Task<IActionResult> Get(string search = "", int? page = 1, bool? desc = false, int? field = 0)
        {
            var response = await GetAll(search, page, desc, field);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await repository.GetAsync(r => r.Id.Equals(id), r => r.OrderBy(x => x.SearchableName), 1);
            var item = result.FirstOrDefault();

            if (item == null) return NotFound();

            return Ok(item);
        }

        [Route("Suggestions")]
        [HttpGet]
        public async Task<IActionResult> Suggestions(string search = "")
        {
            var suggestions = await GetSuggestions(search);

            return Ok(suggestions);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(repository);
        }

        #region HELPER_METHODS
        private async Task<EmailtemplateSearchResponse> GetAll(string search = "", int? page = 1, bool? desc = false, int? sortColumn = 0)
        {
            search = search.ToLower();
            Expression<Func<EmailTemplate, bool>> whereClause = r => search == "" || r.SearchableName.ToLower().Contains(search);

            var orderBy = GetOrderBy((eSortColumn)sortColumn.Value, desc.Value);
            var items = await repository.GetPagedListAsync(page.Value, whereClause, orderBy);

            return new EmailtemplateSearchResponse(items.ToList(), items.TotalItemCount, repository.PageSize);
        }

        private async Task<List<string>> GetSuggestions(string search = "")
        {
            search = search.ToLower();

            return await repository
                .GetAutoCompleteIntellisenseAsync(r => search == "" || r.SearchableName.ToLower().Contains(search),
                    r => r.OrderBy(s => s.SearchableName),
                    r => r.SearchableName);
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
        #endregion // HELPER_METHODS
    }
}
