using Microsoft.Extensions.Configuration;
using EcomEngine.Infrastructure.Configurations;
    
namespace EcomEngine.Infrastructure
{
    /// <summary>
    /// Set in Program.cs
    /// </summary>
    public static class ConnectionStrings
    {
        /// <summary>
        /// Should match the entry in appsettings*.json. Used in Program.cs
        /// </summary>
        public static string EcomEngineDbKey { get; } = "EcomEngineConnectionString";
       
		/// <summary>
        /// Set in Program.cs
        /// </summary>
        public static string EcomEngineDb { get; private set; }

        /// <summary>
        /// Set in Program.cs
        /// </summary>
        public static void SetOneTime(IConfiguration configuration)
        {
			using (var config = new EcomEngineConnectionStringParser(configuration))
            {
                EcomEngineDb = config.Value;
            }
        }
    }
}
