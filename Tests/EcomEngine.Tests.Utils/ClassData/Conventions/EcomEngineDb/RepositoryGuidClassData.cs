using Eml.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using EcomEngine.Business.Common.BaseClasses;
using EcomEngine.Data.Repositories.EcomEngineDb.Contracts;
using EcomEngine.Infrastructure.Contracts;
using EcomEngine.Tests.Utils.BaseClasses;

namespace EcomEngine.Tests.Utils.ClassData.Conventions.EcomEngineDb
{
    public class RepositoryGuidClassData : ClassDataBase<Type>
    {
        private static List<Type> _repositories;

        public RepositoryGuidClassData()
            : base(() =>
            {
                var dataRepositoryGuid = typeof(IEcomEngineDataRepositoryGuid<>);

                return _repositories ?? (_repositories = typeof(EntityGuidBase).Assembly
                           .GetClasses(type => typeof(IEcomEngineDbEntity).IsAssignableFrom(type) 
                                               && !type.IsEnum
                                               && type.Namespace != null
                                               && type.Namespace.Contains("Business.Common.Entities"))
                           .Select(type =>
                           {
                               Type[] typeArgs = {type};

                               return dataRepositoryGuid.MakeGenericType(typeArgs);
                           })
                           .ToList());
            })
        { }
    }
}
