import { Injectable } from "@angular/core";
import { EmailtemplateSearchRequest } from "./requests/emailtemplate-search-request";
import { IEmailTemplate } from "./dto/iemail-template";
import { SearchResponse } from "../shared/responses/search-response";
import { SearchService } from "../shared/services/search.service";

@Injectable()
export class EcomengineService {

    constructor(private readonly searchService: SearchService)
    {
    }

    getSuggestions(query: string) {
        const controller = "emailtemplate";

        return this.searchService.getSuggestions(controller, query);
    }

    search(request: EmailtemplateSearchRequest) {
        const cleanedRequest = this.cleanParameters(request);
        const controller = "emailtemplate";
        return this.searchService.search<EmailtemplateSearchRequest, IEmailTemplate>(controller, cleanedRequest);
    }

    private cleanParameters(request: EmailtemplateSearchRequest): EmailtemplateSearchRequest {
        const cleanedRequest = new EmailtemplateSearchRequest(request.search, request.page, request.desc, request.field);

        if (cleanedRequest.search === "") { cleanedRequest.search = undefined; }
        if (cleanedRequest.page > 1) { } else { cleanedRequest.page = undefined; }
        if (!cleanedRequest.desc) { cleanedRequest.desc = undefined; }
        if (cleanedRequest.desc) { cleanedRequest.desc = true } else { cleanedRequest.desc = undefined; }
        if (cleanedRequest.field > 0) { } else { cleanedRequest.field = undefined; }

        return cleanedRequest;
    }
}
