using Eml.ClassFactory.Contracts;
using Eml.Extensions;
using Xunit;

namespace EcomEngineTechChallenge.Tests.Integration.BaseClasses
{
    [Collection(IntegrationTestDbFixture.COLLECTION_DEFINITION)]
    public abstract class IntegrationTestDbBase
    {
        protected readonly IClassFactory classFactory;

        protected IntegrationTestDbBase()
        {
            classFactory = IntegrationTestDbFixture.ClassFactory;
            classFactory.CheckNotNull("classFactory");
        }
    }
}


