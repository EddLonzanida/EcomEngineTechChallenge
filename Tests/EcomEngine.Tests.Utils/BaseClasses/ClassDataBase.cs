using System;
using System.Collections.Generic;
using Xunit;

namespace EcomEngine.Tests.Utils.BaseClasses
{
    public abstract class ClassDataBase<T> : TheoryData<T>
		where T : class
    {
        protected ClassDataBase(Func<List<T>> getTypes)
        {
            var concreteClasses = getTypes();

            concreteClasses.ForEach(Add);
        }
    }
}
