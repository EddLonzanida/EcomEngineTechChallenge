using Eml.Contracts.Entities;
using Eml.ControllerBase;
using Eml.Contracts.Requests;
using Eml.Contracts.Responses;
using Eml.DataRepository.Contracts;
using EcomEngine.Infrastructure.Contracts;
using EcomEngine.Api.Helpers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngine.Api.Controllers.BaseClasses.EcomEngineDb
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
	[ApiConventionType(typeof(CustomApiConventions))]
    public abstract class CrudControllerApiWithParentSoftDeletableGuidBase<T, TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, TRepository>
        : CrudControllerApiWithParentSoftDeletableBase<Guid, T,TIndexRequest, TIndexResponse, TEditCreateRequest, TDetailsCreateResponse, Data.EcomEngineDb, TRepository>
        where T : class, IEntityWithParentBase<Guid>, IEntitySoftdeletableBase, IEcomEngineDbEntity
        where TIndexRequest : IIndexRequest, new()
        where TIndexResponse : ISearchResponse<T>
        where TEditCreateRequest : class, IEntityBase<Guid>
        where TDetailsCreateResponse : class, IEntityBase<Guid>
        where TRepository : IDataRepositorySoftDeleteBase<Guid, T, Data.EcomEngineDb>
    {
        protected CrudControllerApiWithParentSoftDeletableGuidBase(TRepository repository) 
            : base(repository)
        {
        }

        protected string GetCurrentUser()
        {
            return "";
        }

        protected override string GetDeletedBy()
        {
            return GetCurrentUser();
        }
    }
}
