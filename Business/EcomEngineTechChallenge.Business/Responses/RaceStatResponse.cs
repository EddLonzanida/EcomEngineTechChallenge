using System.Collections.Generic;
using Eml.Mediator.Contracts;
using EcomEngineTechChallenge.Business.Common.Dto;

namespace EcomEngineTechChallenge.Business.Responses
{
    public class RaceStatResponse : IResponse
    {
        public IEnumerable<RaceStat> RaceStats { get; }

        public RaceStatResponse(IEnumerable<RaceStat> raceStats)
        {
            RaceStats = raceStats;
        }
    }
}

