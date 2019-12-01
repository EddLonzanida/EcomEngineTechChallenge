using Eml.Contracts.Entities;
using EcomEngine.Data.Repositories.EcomEngineDb.BaseClasses;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using EcomEngine.Infrastructure.Contracts;
using System;
using System.Composition;

namespace EcomEngine.Data.Repositories.EcomEngineDb
{
    [Export(typeof(IEcomEngineDataRepositoryGuid<>))]
    public class EcomEngineDataRepositoryGuid<T> : EcomEngineDataRepositoryBase<Guid, T>, IEcomEngineDataRepositoryGuid<T>
        where T : class, IEntityBase<Guid>, IEcomEngineDbEntity
    {
    }
}
