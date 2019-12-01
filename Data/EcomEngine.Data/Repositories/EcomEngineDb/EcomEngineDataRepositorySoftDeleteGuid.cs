using Eml.Contracts.Entities;
using EcomEngine.Data.Repositories.EcomEngineDb.BaseClasses;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using EcomEngine.Infrastructure.Contracts;
using System;
using System.Composition;

namespace EcomEngine.Data.Repositories.EcomEngineDb
{
    [Export(typeof(IEcomEngineDataRepositorySoftDeleteGuid<>))]
    public class EcomEngineDataRepositorySoftDeleteGuid<T> : EcomEngineDataRepositorySoftDeleteBase<Guid, T>, IEcomEngineDataRepositorySoftDeleteGuid<T>
        where T : class, IEntityBase<Guid>, IEntitySoftdeletableBase, IEcomEngineDbEntity
    {
    }
}
