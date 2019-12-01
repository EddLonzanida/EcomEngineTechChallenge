using Eml.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using EcomEngine.Api;
using EcomEngine.Tests.Utils.BaseClasses;

namespace EcomEngine.Tests.Utils.ClassData.Conventions
{
    public class ControllerClassData : ClassDataBase<Type>
    {
        private static List<Type> _controllers;

        public ControllerClassData()
            : base(() =>
            {
                return _controllers 
                       ?? (_controllers = typeof(Program).Assembly
                           .GetClasses(type => typeof(ControllerBase).IsAssignableFrom(type)));
            })
        { }
    }
}
