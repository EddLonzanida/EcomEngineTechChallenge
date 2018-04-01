import { Component, OnInit } from "@angular/core";
import { EcomengineService } from "./ecomengine.service";
import { EmailtemplateSearchRequest } from "./requests/emailtemplate-search-request";
import { EmailtemplateSearchResponse } from "./responses/emailtemplate-search-response";

@Component({
    selector: "at-ecomengine",
    templateUrl: "./ecomengine.component.html",
    styleUrls: ["./ecomengine.component.css"]
})
export class EcomengineComponent implements OnInit {
    // search parameters
    searchName: string;
    // auto complete
    searchSuggestions: string[];
    // search parameters
    searchRequest = new EmailtemplateSearchRequest();
    // search responses
    searchResponse = new EmailtemplateSearchResponse();
    // busyIndicator
    isBusy = false;
    canShowPager = false;
    hasErrors = false;
    initialLoad = true;
    // column headers
    cols = [
        { field: "emailLabel", header: "Email Label" },
        { field: "fromAddress", header: "From Address" },
        { field: "dateUpdated", header: "Date Updated" }
    ];

    constructor(private readonly ecomengineService: EcomengineService) {
        this.searchSuggestions = [];
        this.initialLoad = true;
    }

    ngOnInit() {
        this.search();
    }

    search(): void {
        this.isBusy = true;
        this.hasErrors = false;
        this.ecomengineService.search(this.searchRequest)
            .then(r => {
                this.isBusy = false;
                this.searchResponse = r;
                this.canShowPager = this.searchResponse.recordCount > this.searchResponse.rowsPerPage;

                if (!this.canShowPager) {
                    this.searchRequest.page = 1;
                }
                this.initialLoad = false;
            })
            .catch(e => {
                this.isBusy = false;
                this.hasErrors = true;
            });
    }

    getSuggestions(event): void {
        const query = event.query;
        this.ecomengineService.getSuggestions(query).then(suggestions => {
            this.searchSuggestions = suggestions;
        })
            .catch(e => {
                console.error(e);
            });
    }

    onSearchClicked() {
        this.searchRequest.page = 1;
        this.initialLoad = false;
        this.search();
    }

    onLazyLoad(event) {
        if (this.initialLoad) {
            return;
        }

        if (event && event.first > 0 && event.rows > 0) {
            this.searchRequest.page = (event.first / event.rows) + 1;
        } else {
            this.searchRequest.page = 1;
        }

        if (event.sortField) {
            this.searchRequest.field = this.cols.findIndex(c => event.sortField === c.field);
            this.searchRequest.desc = !(event.sortOrder === -1);
        }

        this.search();
    }
}
