using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Responses;

namespace EcomEngineTechChallenge.Business.Requests
{
    public class TotalBetCountRequest : IRequestAsync<TotalBetCountRequest, TotalBetCountResponse>
    {
        public int CustomerId { get; set; }

        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="TotalBetCountEngine"/>.
        /// </summary>
        public TotalBetCountRequest(int customerId, int pageNumber)
        {
            CustomerId = customerId;
            PageNumber = pageNumber;
        }
    }
}

