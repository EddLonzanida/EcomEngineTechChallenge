using Shouldly;
using System;
using EcomEngine.Business.Common.BaseClasses;
using EcomEngine.Tests.Utils.ClassData.Conventions;
using Xunit;

namespace EcomEngine.Tests.Unit.Conventions.Entities
{
    public class EntityTests
    {
        [Theory]
        [ClassData(typeof(BusinessCommonEntitiesClassData))]
        public void Entities_ShouldBeInBusinessCommon(Type type)
        {
            var nameSpace = type.Namespace;

            nameSpace.ShouldNotBeNull();

            var isInBusinessCommonNamespace = nameSpace.Contains(".Entities");

            isInBusinessCommonNamespace.ShouldBeTrue();
        }

        [Theory]
        [ClassData(typeof(InheritsFromEntityGuidBaseClassData))]
        public void Entities_ShouldInheritFromEntityGuidBase(Type type)
        {
            var isAssignableToEntityBase = typeof(EntityGuidBase).IsAssignableFrom(type);

            isAssignableToEntityBase.ShouldBeTrue();
        }
    }
}
