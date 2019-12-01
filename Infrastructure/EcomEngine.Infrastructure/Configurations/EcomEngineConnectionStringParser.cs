using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;
using System.Composition;

namespace EcomEngine.Infrastructure.Configurations
{
    public class EcomEngineConnectionStringParser : ConfigParserBase<string, EcomEngineConnectionStringParser>
    {
        /// <summary>
        /// DI signature: <![CDATA[IConfigParserBase<string, EcomEngineConnectionStringParser> ecomengineConnectionStringParser]]>.
        /// </summary>
        [ImportingConstructor]
        public EcomEngineConnectionStringParser(IConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
