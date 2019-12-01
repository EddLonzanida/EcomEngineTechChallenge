using Eml.DataRepository.Attributes;
using EcomEngine.Infrastructure;
using Eml.DataRepository.BaseClasses;

namespace EcomEngine.DataMigration.Migrators
{
    [DbMigratorExport(DbNames.EcomEngine)]
    public class EcomEngineDbMigrator : MigratorBase<EcomEngineDb>
    {
    }
}
