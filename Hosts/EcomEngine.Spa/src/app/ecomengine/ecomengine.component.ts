import { Component, OnInit, ChangeDetectorRef, OnDestroy } from "@angular/core";
import { EcomengineService } from "./services/ecomengine.service";
import { EmailtemplateSearchRequest } from "./requests/emailtemplate-search-request";
import { IEmailTemplate } from "./dto/iemail-template";
import { SearchResponse } from "../shared/responses/search-response";
import { Observable, Subscription } from 'rxjs';

@Component({
    selector: "at-ecomengine",
    templateUrl: "./ecomengine.component.html",
    styleUrls: ["./ecomengine.component.css"]
})
export class EcomengineComponent implements OnInit, OnDestroy {
    
    private subcriptions: Subscription = new Subscription();
    
    // search parameters
    searchName: string;
    // auto complete
    searchSuggestions: string[];
    // search parameters
    searchRequest = new EmailtemplateSearchRequest();
    // search responses
    searchResponse = new SearchResponse<IEmailTemplate>();
    // busyIndicator
    isBusy = true;
    canShowPager = false;
    hasErrors = false;
    initialLoad = true;
    // column headers
    cols = [
        { field: "emailLabel", header: "Email Label" },
        { field: "fromAddress", header: "From Address" },
        { field: "dateUpdated", header: "Date Updated" }
    ];

    constructor(private readonly ecomengineService: EcomengineService, private readonly cd: ChangeDetectorRef) {
    }

    ngOnInit() {

        this.search();

    }

    search() {

        this.isBusy = true;
        this.hasErrors = false;

        this.subcriptions.add(this.ecomengineService.search(this.searchRequest)
            .subscribe(r => {
                this.searchResponse = r;
                this.canShowPager = this.searchResponse.recordCount > this.searchResponse.rowsPerPage;

                if (!this.canShowPager) this.searchRequest.page = 1;

                const initial_load = this.initialLoad;
                this.initialLoad = false;
                this.isBusy = false;

                if (initial_load) this.cd.detectChanges(); //prevent ExpressionChangedAfterItHasBeenCheckedError

            }, error => this.handleError()));
    }

    private handleError() {

        this.initialLoad = true;
        this.hasErrors = true;
        this.isBusy = false;
        this.cd.detectChanges();

    }

    getSuggestions(event): void {

        const query = event.query;

        this.subcriptions.add(this.ecomengineService.getSuggestions(query)
            .subscribe(suggestions => {
                this.searchSuggestions = suggestions;
            }));
    }

    onSearchClicked() {

        this.searchRequest.page = 1;
        this.search();

    }

    onLazyLoad(event) {

        if (this.initialLoad) return;

        if (event && event.first > 0 && event.rows > 0) {

            this.searchRequest.page = (event.first / event.rows) + 1;

        } else {

            this.searchRequest.page = 1;

        }

        if (event.sortField) {

            this.searchRequest.sortColumn = event.sortField;
            this.searchRequest.isDescending = !(event.sortOrder === -1);

        }

        this.search();
    }

    ngOnDestroy(): void {
        this.subcriptions.unsubscribe();
    }
}
