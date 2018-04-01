import { IEmailTemplate } from "../dto/iemail-template";

export class EmailtemplateSearchResponse {
    constructor(public recordCount = 0,
        public rowsPerPage = 0,
        public emailTemplates: IEmailTemplate[] = []) { }
}
