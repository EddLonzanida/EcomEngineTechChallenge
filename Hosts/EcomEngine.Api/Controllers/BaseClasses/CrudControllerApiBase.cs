using EcomEngine.Data;
using EcomEngine.Data.Contracts;
using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using System;
using System.Threading.Tasks;

namespace EcomEngine.Api.Controllers.BaseClasses
{
    public abstract class CrudControllerApiBase<T, TRequest> : CrudControllerApiBase<Guid, T, TRequest, EcomEngineDb, IDataRepositorySoftDeleteGuid<T>>
        where T : class, IEntityBase<Guid>, ISearchableName, IEntitySoftdeletableBase
        where TRequest : class
    {
        protected CrudControllerApiBase(IDataRepositorySoftDeleteGuid<T> repository)
            : base(repository)
        {
        }

        protected CrudControllerApiBase(IMediator mediator, IDataRepositorySoftDeleteGuid<T> repository)
            : base(mediator, repository)
        {
        }

        protected override async Task<T> DeleteItemAsync(EcomEngineDb db, Guid id, string reason)
        {
            return await repository.DeleteAsync(db, id, reason);
        }
    }
}
