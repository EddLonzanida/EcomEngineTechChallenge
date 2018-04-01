using System;

namespace EcomEngineTechChallenge.Business.Common.Dto
{
    public class Rootobject
    {
        public ArrayOfEmailTemplate ArrayOfEmailTemplate { get; set; }
    }

    public class ArrayOfEmailTemplate
    {
        public Emailtemplate[] EmailTemplate { get; set; }
    }

    public class Emailtemplate
    {
        public string Id { get; set; }
        public string EmailLabel { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string TemplateText { get; set; }
        public string EmailType { get; set; }
        public string Active { get; set; }
        public DateTime DateUpdated { get; set; }
        public string LoadDrafts { get; set; }
        public string ParentId { get; set; }
        public string VersionCount { get; set; }
        public string IsDefault { get; set; }
        public string BccAddress { get; set; }
        public Versions Versions { get; set; }
    }

    public class Versions
    {
        public Emailtemplate[] EmailTemplate { get; set; }
    }
}
