using Eml.Contracts.Entities;
using Eml.DataRepository.MsSql.BaseClasses;
using EcomEngine.Infrastructure;
using EcomEngine.Infrastructure.Contracts;

namespace EcomEngine.Data.Repositories.EcomEngineDb.BaseClasses
{
    public abstract class EcomEngineDataRepositorySoftDeleteBase<TUniqueId, T> : DataRepositoryMsSqlSoftDeleteBase<TUniqueId, T, Data.EcomEngineDb>
        where T : class, IEntityBase<TUniqueId>, IEntitySoftdeletableBase, IEcomEngineDbEntity
    {
        public override int GetIntellisenseSize()
        {
            return ApplicationSettings.Config.IntellisenseCount;
        }

        public override string GetConnectionString()
        {
            return ConnectionStrings.EcomEngineDbKey;
        }

        public override int GetPageSize()
        {
            return ApplicationSettings.Config.PageSize;
        }
    }
}
