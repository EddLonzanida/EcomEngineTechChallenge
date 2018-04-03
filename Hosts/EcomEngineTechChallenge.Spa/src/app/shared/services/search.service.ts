import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { HttpClient } from "@angular/common/http";
import "rxjs/add/operator/toPromise";
import { SearchResponse } from "../responses/search-response";

@Injectable()
export class SearchService {
    private readonly baseUrl: string;
    constructor(private readonly http: Http, private readonly httpClient: HttpClient, @Inject("BASE_URL") baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getSuggestions(controller: string, query: string) {
        const action = "suggestions";
        const url = `${this.baseUrl}${controller}/${action}?search=${query}`;

        return this.http.get(url)
            .toPromise()
            .then(res => {
                return <string[]>res.json();
            })
            .then(data => { return data; });
    }

    search<TRequest, TResponse>(controller: string, request: TRequest) {
        const config = { params: request }

        return this.http.get(`${this.baseUrl}${controller}`, config)
            .toPromise()
            .then(res => {
                return res.json() as SearchResponse<TResponse>;
            })
            .then(data => { return data; });
    }
}
