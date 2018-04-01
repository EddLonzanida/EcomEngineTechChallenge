using System;
using System.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Contracts;
using Microsoft.Extensions.Configuration;

namespace EcomEngineTechChallenge.Data.Repositories
{
    [Export(typeof(IDataRepositoryGuid<>))] 
    public class DataRepositoryGuid<T> : DataRepositoryGuid<T, EcomEngineTechChallengeDb>
        where T : class, IEntityBase<Guid>
    {
        [ImportingConstructor]
        public DataRepositoryGuid(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

