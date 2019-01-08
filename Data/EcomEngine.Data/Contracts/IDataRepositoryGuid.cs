using System;
using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace EcomEngine.Data.Contracts
{
    public interface IDataRepositoryGuid<T> : IDataRepositoryGuid<T, EcomEngineDb>
        where T : class, IEntityBase<Guid>
    {
    }
}
