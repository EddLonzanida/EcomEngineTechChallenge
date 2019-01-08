using Eml.Contracts.Entities;
using Eml.DataRepository;
using EcomEngine.Data.Contracts;
using Microsoft.Extensions.Configuration;
using System.Composition;
using System;

namespace EcomEngine.Data.Repositories
{
    [Export(typeof(IDataRepositoryGuid<>))] 
    public class DataRepositoryGuid<T> : DataRepositoryGuid<T, EcomEngineDb>, IDataRepositoryGuid<T>
        where T : class, IEntityBase<Guid>
    {
        [ImportingConstructor]
        public DataRepositoryGuid(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
