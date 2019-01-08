using System;
using System.Threading.Tasks;
using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Mediator.Contracts;
using EcomEngine.Data;
using EcomEngine.Data.Contracts;

namespace EcomEngine.Api.Controllers.BaseClasses
{
    public abstract class CrudControllerApiWithParentBase<T, TRequest> : CrudControllerApiWithParentBase<Guid, T, TRequest, EcomEngineDb, IDataRepositorySoftDeleteGuid<T>>
        where T : class, IEntityWithParentBase<Guid>, ISearchableName, IEntitySoftdeletableBase
        where TRequest : class
    {
        protected CrudControllerApiWithParentBase(IDataRepositorySoftDeleteGuid<T> repository) : base(repository)
        {
        }

        protected CrudControllerApiWithParentBase(IMediator mediator, IDataRepositorySoftDeleteGuid<T> repository) : base(mediator, repository)
        {
        }

        protected override async Task<T> DeleteItemAsync(EcomEngineDb db, Guid id, string reason)
        {
            return await repository.DeleteAsync(db, id, reason);
        }
    }
}
