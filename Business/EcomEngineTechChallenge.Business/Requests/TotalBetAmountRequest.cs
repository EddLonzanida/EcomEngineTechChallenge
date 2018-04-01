using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Responses;

namespace EcomEngineTechChallenge.Business.Requests
{
    public class TotalBetAmountRequest : IRequestAsync<TotalBetAmountRequest, TotalBetAmountResponse>
    {
        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="TotalBetAmountEngine"/>.
        /// </summary>
        public TotalBetAmountRequest()
        {
            PageNumber = 1;
        }
    }
}

