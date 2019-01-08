using System;
using System.Linq;
using EcomEngine.Data.Contracts;
using Eml.EntityBaseClasses;
using Eml.Extensions;
using Xunit;

namespace EcomEngine.Tests.Integration.ClassData
{
    public class RepositoryClassData : TheoryData<Type>
    {
        private const string NAMESPACE = "EcomEngine.Business.Common";
       
		public RepositoryClassData()
        {
            var dataRepositoryInt = typeof(IDataRepositorySoftDeleteGuid<>);
            var concreteClasses = TypeExtensions.GetReferencingAssemblies(r => r.Name.Equals(NAMESPACE))
                .First()
                .GetClasses(type => !type.IsAbstract 
                                    && typeof(EntityBaseGuid).IsAssignableFrom(type) 
                                    && !type.IsEnum
                                    && type.Namespace.EndsWith("Entities"))
                .Select(type =>
                {
                    Type[] typeArgs = { type };

                    return dataRepositoryInt.MakeGenericType(typeArgs);
                });

            foreach (var type in concreteClasses)
            {
                Add(type);
            }
        }
    }
}
