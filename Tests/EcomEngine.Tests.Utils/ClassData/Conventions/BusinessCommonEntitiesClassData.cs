using Eml.Extensions;
using System;
using System.Collections.Generic;
using EcomEngine.Infrastructure;
using EcomEngine.Tests.Utils.BaseClasses;

namespace EcomEngine.Tests.Utils.ClassData.Conventions
{
    public class BusinessCommonEntitiesClassData : ClassDataBase<Type>
    {
        private static List<Type> _entityClasses;

        public BusinessCommonEntitiesClassData()
            : base(() =>
            {
                if (_entityClasses != null) return _entityClasses;

                var assemblyPattern = new UniqueStringPattern(new List<string> { $"{Constants.ApplicationId}." }).Build();
                var assemblies = TypeExtensions.GetReferencingAssemblies(assemblyPattern);

                _entityClasses = assemblies.GetClasses(type => !string.IsNullOrWhiteSpace(type.Namespace) 
                                                                && type.Namespace.Contains(".Entities")
                                                                && !type.Namespace.Contains(".Dto.")
                                                                && !type.Namespace.Contains("Test")
                                                                && !type.IsEnum);                                                

                return _entityClasses;
            })
        { }
    }
}
