using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.Contracts;
using EcomEngine.Infrastructure.Contracts;
using System;

namespace EcomEngine.Data.Repositories.EcomEngineDb.Contracts
{
    public interface IEcomEngineDataRepositorySoftDeleteGuid<T> 
        : IEcomEngineDataRepositoryGuid<T>, IDataRepositoryMsSqlSoftDeleteGuid<T, Data.EcomEngineDb>
        where T : class, IEntityBase<Guid>, IEntitySoftdeletableBase, IEcomEngineDbEntity
    {
    }
}
