using System.Collections.Generic;
using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Common.Dto;

namespace EcomEngineTechChallenge.Business.Responses
{
    public class RiskCustomerResponse : IResponse
    {
        public IEnumerable<RiskCustomer> RiskCustomers { get; }

        public RiskCustomerResponse(IEnumerable<RiskCustomer> riskCustomers)
        {
            RiskCustomers = riskCustomers;
        }
    }
}

