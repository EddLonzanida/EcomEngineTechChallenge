using System.Composition;
using EcomEngine.Data;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using EcomEngine.DataMigration.BaseClasses;

namespace EcomEngine.DataMigration
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigrator : MainDbMigratorBase<EcomEngineDb>
    {
		[ImportingConstructor]
        public MainDbMigrator(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString)
        {
        }

        protected override void Seed(EcomEngineDb context)
        {
        }
    }
}
