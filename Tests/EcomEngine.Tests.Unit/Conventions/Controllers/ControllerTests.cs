using System;
using System.Composition;
using Eml.Extensions;
using Shouldly;
using EcomEngine.Tests.Utils.ClassData.Conventions;
using Xunit;
    
    namespace EcomEngine.Tests.Unit.Conventions.Controllers
    {
        public class ControllerTests
        {
            [Theory]
            [ClassData(typeof(ControllerClassData))]
            public void Controller_ShouldHaveExportAttributes(Type type)
            {
                var sut = type.GetClassAttribute<ExportAttribute>();
    
                sut.ShouldNotBeNull();
            }
        }
    }
    
