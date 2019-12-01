using System.Collections.Generic;
using AspNetCoreRateLimit;
using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;
using System.Composition;

namespace EcomEngine.Api.Configurations
{
    public class RateLimitsConfig : ConfigParserBase<List<RateLimitRule>, RateLimitsConfig>
    {
        /// <summary>
        /// DI signature: <![CDATA[IConfigParserBase<List<RateLimitRule>, RateLimitsConfig> rateLimitsConfig]]>.
        /// </summary>
        [ImportingConstructor]
        public RateLimitsConfig(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
