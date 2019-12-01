using EcomEngine.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EcomEngine.Infrastructure
{
    public static class Constants
    {
        /// <summary>
        /// Used in MEF DI, Swagger, Integration test, Unit tests
        /// </summary>
        public const string ApplicationId = "EcomEngine";

        /// <summary>
        /// Default value is "Development". Set in Program.cs
        /// </summary>
        public static string CurrentEnvironment = "Development";
    }

    /// Database id for DbMigratorExportAttribute.
    public static class DbNames
    {
        public const string EcomEngine = "EcomEngine";
    }
}
