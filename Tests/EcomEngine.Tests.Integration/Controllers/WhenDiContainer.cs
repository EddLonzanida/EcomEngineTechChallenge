using Eml.Extensions;
using EcomEngine.Tests.Integration.BaseClasses;
using System;
using System.Composition;
using EcomEngine.Tests.Utils.ClassData.Conventions;
using Shouldly;
using Xunit;

namespace EcomEngine.Tests.Integration.Controllers
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldBeExportable(Type type)
        {
            classFactory.Container.TryGetExport(type, out var sut);

            sut.ShouldNotBeNull();
        }

        [Theory]
        [ClassData(typeof(ControllerClassData))]
        public void Controller_ShouldHaveExportAttributes(Type type)
        {
            var sut = type.GetClassAttribute<ExportAttribute>();

            sut.ShouldNotBeNull();
        }
    }
}
