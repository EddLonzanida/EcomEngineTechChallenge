using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Contracts;
using Eml.ControllerBase;
using Eml.DataRepository.Contracts;

namespace EcomEngineTechChallenge.ApiHost.Controllers
{
    [Export]
    public class EmailTemplateController : CrudControllerBaseGuid<EmailTemplate, IDataRepositoryGuid<EmailTemplate>>
    {
        [ImportingConstructor]
        public EmailTemplateController(IDataRepositoryGuid<EmailTemplate> repository)
        : base(repository)
        {
        }

        protected override async Task<SearchResponse<EmailTemplate>> GetAll(string search = "", int? page = 1, bool? desc = false, int? sortColumn = 0, Guid? parentId = null)
        {
            search = search.ToLower();
            Expression<Func<EmailTemplate, bool>> whereClause = r => search == "" || r.SearchableName.ToLower().Contains(search);

            var orderBy = GetOrderBy((eSortColumn)sortColumn.Value, desc.Value);
            var items = await repository.GetPagedListAsync(page.Value, whereClause, orderBy);

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
