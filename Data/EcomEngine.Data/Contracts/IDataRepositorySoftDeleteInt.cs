using System;
using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace EcomEngine.Data.Contracts
{
    public interface IDataRepositorySoftDeleteGuid<T> : IDataRepositorySoftDeleteGuid<T, EcomEngineDb>
        where T : class, IEntityBase<Guid>, IEntitySoftdeletableBase
    {
    }
}
