export interface IEmailTemplate {
    id: string;
    parentId: string;
    emailLabel: string;
    fromAddress: string;
    subject: string;
    templateText: string;
    emailType: string;
    active: boolean;
    dateUpdated: Date;
    loadDrafts: boolean;
    isDefault: boolean;
    bccAddress: string;
    versionCount: number;
}
