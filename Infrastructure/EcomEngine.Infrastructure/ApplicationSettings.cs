using EcomEngine.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;

namespace EcomEngine.Infrastructure
{
    /// <summary>
    /// Set in Program.cs
    /// </summary>
	public static class ApplicationSettings
    {
        /// <summary>
        /// Set in Program.cs
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Set in Program.cs
        /// </summary>
        public static ApplicationSettingsConfig Config { get; private set; }

        /// <summary>
        /// Set in Program.cs
        /// </summary>
        public static void SetOneTime(IConfiguration configuration)
        {
            Configuration = configuration;
           
			using (var config = new ApplicationSettingsConfigParser(configuration))
            {
                Config = config.Value;
            }
        }
    }
}
