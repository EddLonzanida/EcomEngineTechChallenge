using System;
using Eml.Contracts.Entities;

namespace EcomEngine.Business.Common.Dto.EcomEngineDb
{
    public class EmailTemplateEditCreateRequest : IEntityBase<Guid>
    {
        public string Name { get; set; }
      
        public Guid Id { get; set; }
    }
}
