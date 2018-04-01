using System;
using System.Collections.Generic;
using Eml.ExplicitDispose.Api;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngineTechChallenge.ApiHost.BaseClasses
{
    [ExplicitDispose]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller, IDisposeAware
    {
        /// <summary>
        /// Ex: Disposables.Add([Concrete classes that implements IDisposable]);
        /// </summary>
        /// <param name="disposables"></param>
        protected abstract void RegisterIDisposable(List<IDisposable> disposables);

        public List<IDisposable> Disposables { get; private set; } = new List<IDisposable>();

        [ApiExplorerSettings(IgnoreApi = true)]
        public void RegisterDisposables(List<IDisposable> disposables)
        {
            RegisterIDisposable(disposables);
        }
    }
}


