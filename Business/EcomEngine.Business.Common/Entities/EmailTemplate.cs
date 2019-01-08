using EcomEngine.Business.Common.BaseClasses;
using Eml.Contracts.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomEngine.Business.Common.Entities
{
    public class EmailTemplate : EntityBase, ISearchableName
    {
        public Guid? ParentId { get; set; }

        public string EmailLabel { get; set; }

        [EmailAddress]
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string TemplateText { get; set; }
        public string EmailType { get; set; }
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateUpdated { get; set; }

        public bool LoadDrafts { get; set; }
        public bool IsDefault { get; set; }

        [EmailAddress]
        public string BccAddress { get; set; }

        public int VersionCount { get; set; }

        public virtual List<EmailTemplate> Versions { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string SearchableName => EmailLabel;
    }
}
