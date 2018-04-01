using System.Collections.Generic;
using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Common.Dto;

namespace EcomEngineTechChallenge.Business.Responses
{
    public class TotalBetCountResponse : IResponse
    {
        public IEnumerable<CustomerBetCount> BetCounts { get; }

        public TotalBetCountResponse(IEnumerable<CustomerBetCount> betCounts)
        {
            BetCounts = betCounts;
        }
    }
}

