using EcomEngine.Business.Common.Entities.EcomEngineDb;

namespace EcomEngine.Business.Common.Dto.EcomEngineDb.EntityHelpers
{
    public static class EmailTemplateExtensions
    {
        public static EmailTemplate ToEntity(this EmailTemplateEditCreateRequest dto)
        {
            return new EmailTemplate
            {
                Id = dto.Id,
                EmailLabel = dto.Name
            };
        }

        public static EmailTemplateDetailsCreateResponse ToDto(this EmailTemplate entity)
        {
            return new EmailTemplateDetailsCreateResponse
            {
                Id = entity.Id,
                EmailLabel = entity.EmailLabel
            };
        }
    }
}
