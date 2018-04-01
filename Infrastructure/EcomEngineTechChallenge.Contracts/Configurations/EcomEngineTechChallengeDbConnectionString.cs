using Eml.ConfigParser;
using Eml.ConfigParser.Parsers;
using Microsoft.Extensions.Configuration;

namespace EcomEngineTechChallenge.Contracts.Configurations
{
    public class EcomEngineTechChallengeDbConnectionString : ConfigBase<string, EcomEngineTechChallengeDbConnectionString>
    {
        public EcomEngineTechChallengeDbConnectionString(IConfiguration configuration, IConfigParser customParser = null)
            : base(configuration, customParser)
        {
        }
    }
}

