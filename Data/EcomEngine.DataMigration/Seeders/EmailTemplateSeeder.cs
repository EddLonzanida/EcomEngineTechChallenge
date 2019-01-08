using System;
using System.Collections.Generic;
using System.Linq;
using EcomEngine.Business.Common.Dto;
using EcomEngine.Business.Common.Entities;
using Eml.DataRepository;

namespace EcomEngine.DataMigration.Seeders
{
    public static class EmailTemplateSeeder
    {
        public static void Seed(EcomEngineDb context, string relativePath)
        {
            Seeder.Execute("EmailTemplates", () =>
            {
                var initialData = GetEmailTemplatesFromJson(relativePath);

                context.AddRange(initialData);
                context.SaveChanges();
            });
        }

        public static List<EmailTemplate> GetEmailTemplatesFromJson(string relativePath)
        {
            var initialData = Seeder.GetJsonStub<Rootobject>("Data", relativePath);
            var items = new List<EmailTemplate>();
            var rnd = new Random();
            const int MAX = 150;

            initialData.ArrayOfEmailTemplate.EmailTemplate.ToList().ForEach(e =>
            {

                var item = new EmailTemplate
                {
                    ParentId = Guid.Parse(e.ParentId),
                    Id = Guid.Parse(e.Id),
                    EmailLabel = e.EmailLabel,
                    FromAddress = $"{rnd.Next(MAX)}{e.FromAddress}",
                    Subject = e.Subject,
                    TemplateText = e.TemplateText,
                    EmailType = e.EmailType,
                    Active = bool.Parse(e.Active),
                    DateUpdated = e.DateUpdated.AddHours(rnd.Next(MAX)),
                    LoadDrafts = bool.Parse(e.LoadDrafts),
                    IsDefault = bool.Parse(e.IsDefault),
                    BccAddress = e.BccAddress,
                    VersionCount = int.Parse(e.VersionCount)
                };

                if (e.Versions.EmailTemplate.Any())
                {
                    e.Versions.EmailTemplate.ToList().ForEach(v =>
                    {
                        item.Versions = new List<EmailTemplate>();

                        item.Versions.Add(new EmailTemplate
                        {
                            ParentId = Guid.Parse(v.ParentId),
                            Id = Guid.Parse(v.Id),
                            EmailLabel = v.EmailLabel,
                            FromAddress = $"{rnd.Next(MAX)}{v.FromAddress}",
                            Subject = v.Subject,
                            TemplateText = v.TemplateText,
                            EmailType = v.EmailType,
                            Active = bool.Parse(v.Active),
                            DateUpdated = v.DateUpdated.AddHours(rnd.Next(MAX)),
                            LoadDrafts = bool.Parse(v.LoadDrafts),
                            IsDefault = bool.Parse(v.IsDefault),
                            BccAddress = v.BccAddress,
                            VersionCount = int.Parse(v.VersionCount)
                        });
                    });
                }

                items.Add(item);
            });

            return items;
        }
    }
}
