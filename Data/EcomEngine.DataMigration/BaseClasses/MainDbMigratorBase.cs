using Eml.DataRepository;
using Eml.DataRepository.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace EcomEngine.DataMigration.BaseClasses
{
    public abstract class MainDbMigratorBase<T> : MigratorBase<T>
        where T : DbContext, new()
    {
        protected MainDbMigratorBase(MainDbConnectionString mainDbConnectionString)
           : base(mainDbConnectionString.Value)
        {
        }
    }
}
