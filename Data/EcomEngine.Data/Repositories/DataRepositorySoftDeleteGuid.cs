using Eml.Contracts.Entities;
using Eml.DataRepository;
using EcomEngine.Data.Contracts;
using Microsoft.Extensions.Configuration;
using System.Composition;
using System;

namespace EcomEngine.Data.Repositories
{
    [Export(typeof(IDataRepositorySoftDeleteGuid<>))] 
    public class DataRepositorySoftDeleteGuid<T> : DataRepositorySoftDeleteGuid<T, EcomEngineDb>, IDataRepositorySoftDeleteGuid<T>
        where T : class, IEntityBase<Guid>, IEntitySoftdeletableBase
    {
        [ImportingConstructor]
        public DataRepositorySoftDeleteGuid(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

