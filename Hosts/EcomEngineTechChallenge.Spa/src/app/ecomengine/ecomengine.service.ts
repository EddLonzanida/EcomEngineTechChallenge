import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { HttpClient } from "@angular/common/http";
import "rxjs/add/operator/toPromise";
import { EmailtemplateSearchRequest } from "./requests/emailtemplate-search-request";
import { EmailtemplateSearchResponse } from "./responses/emailtemplate-search-response";

@Injectable()
export class EcomengineService {
    private readonly baseUrl: string;
    constructor(private readonly http: Http, private readonly httpClient: HttpClient, @Inject("BASE_URL") baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getSuggestions(query: string) {
        const action = "suggestions";
        const url = `${this.baseUrl}emailtemplate/${action}?search=${query}`;

        return this.http.get(url)
            .toPromise()
            .then(res => {
                return <string[]>res.json();
            })
            .then(data => { return data; });
    }

    search(request: EmailtemplateSearchRequest) {
        const cleanedRequest = this.cleanParameters(request);
        const config = { params: cleanedRequest }

        return this.http.get(`${this.baseUrl}emailtemplate`, config)
            .toPromise()
            .then(res => {
                return <EmailtemplateSearchResponse>res.json();
            })
            .then(data => { return data; });
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
