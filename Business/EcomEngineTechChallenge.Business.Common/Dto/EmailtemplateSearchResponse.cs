using System.Collections.Generic;
using EcomEngineTechChallenge.Business.Common.Entities;
using System.Linq;

namespace EcomEngineTechChallenge.Business.Common.Dto
{
    public class EmailtemplateSearchResponse
    {
        public List<EmailTemplate> Emailtemplates { get; }

        public int RecordCount { get; }

        public int RowsPerPage { get; }

        public EmailtemplateSearchResponse(IEnumerable<EmailTemplate> emailtemplates, int recordCount, int rowsPerPage)
        {
            Emailtemplates = emailtemplates.ToList();
            RecordCount = recordCount;
            RowsPerPage = rowsPerPage;
        }
    }
}
