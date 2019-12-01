using System.Collections.Generic;
using Eml.Contracts.Responses;
using EcomEngine.Business.Common.Entities.EcomEngineDb;

namespace EcomEngine.Business.Common.Dto.EcomEngineDb
{
    public class EmailTemplateIndexResponse : SearchResponse<EmailTemplate>
    {
        public EmailTemplateIndexResponse(IEnumerable<EmailTemplate> items, int recordCount, int rowsPerPage) 
            : base(items, recordCount, rowsPerPage)
        {
        }
    }
}
