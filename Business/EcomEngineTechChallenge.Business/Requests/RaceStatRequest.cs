using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Responses;

namespace EcomEngineTechChallenge.Business.Requests
{
    public class RaceStatRequest : IRequestAsync<RaceStatRequest, RaceStatResponse>
    {
        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="RaceStatEngine"/>.
        /// </summary>
        public RaceStatRequest(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}

