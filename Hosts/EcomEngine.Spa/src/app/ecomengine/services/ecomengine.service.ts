import { Injectable } from "@angular/core";
import { EmailtemplateSearchRequest } from "../requests/emailtemplate-search-request";
import { IEmailTemplate } from "../dto/iemail-template";
import { SearchResponse } from "../../shared/responses/search-response";
import { SearchService } from "../../shared/services/search.service";

@Injectable()
export class EcomengineService {

    constructor(private readonly searchService: SearchService)
    {
    }

    getSuggestions(query: string) {

        const route = "emailtemplate";

        return this.searchService.getSuggestions(route, query);
    }

    search(request: EmailtemplateSearchRequest) {

        const cleanedRequest = this.cleanParameters(request);
        const route = "emailtemplate";

        return this.searchService.search<EmailtemplateSearchRequest, IEmailTemplate>( route, cleanedRequest);

    }
   
    private cleanParameters(request: EmailtemplateSearchRequest): EmailtemplateSearchRequest {

        const cleanedRequest = new EmailtemplateSearchRequest(request.search, request.page, request.isDescending, request.sortColumn);

        if (cleanedRequest.search === "") { cleanedRequest.search = undefined; }
        if (cleanedRequest.page > 1) { } else { cleanedRequest.page = undefined; }
        if (!cleanedRequest.isDescending) { cleanedRequest.isDescending = undefined; }
        if (cleanedRequest.isDescending) { cleanedRequest.isDescending = true } else { cleanedRequest.isDescending = undefined; }
        if (cleanedRequest.sortColumn === 'emailLabel') {  cleanedRequest.sortColumn = undefined;} 
        if (cleanedRequest.sortColumn === '') {  cleanedRequest.sortColumn = undefined;} 

            return cleanedRequest;
        }
    }
    