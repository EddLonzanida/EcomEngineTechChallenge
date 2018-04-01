using System.Collections.Generic;
using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Common.Dto;

namespace EcomEngineTechChallenge.Business.Responses
{
   public class TotalBetAmountResponse : IResponse
    {
        public IEnumerable<CustomerBetAmount> CustomerBets { get; }

        public TotalBetAmountResponse(IEnumerable<CustomerBetAmount> customerBets)
        {
            CustomerBets = customerBets;
        }
    }
}

