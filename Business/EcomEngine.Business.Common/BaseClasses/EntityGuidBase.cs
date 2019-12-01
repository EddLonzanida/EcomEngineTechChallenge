using System;
using Eml.Contracts.Entities;

namespace EcomEngine.Business.Common.BaseClasses
{
    public abstract class EntityGuidBase : IEntityBase<Guid>
    {
        public virtual Guid Id { get; set; }
    }		
}
