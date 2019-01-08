function CreateFolder($path) {
    
    if (!(Test-Path $path)) {
    
        New-Item -ItemType Directory -Force -Path $path
    
    }
}

function DeleteFile($path){
    
  if (Test-Path $path) { 
    
    try {
    
        Write-Host "Deleting $path.." -foreground "magenta" 
            Remove-Item -Path "$path" -Force -ErrorAction SilentlyContinue
            Write-Host " "
        
            return $true
        }
        catch {
    
            $_
            return $false
        }	
    }
    else {
        return $true
    }
}

function DeleteDirectory($path) {
    
    if (Test-Path $path) { 
    
        try {
    
            Write-Host "Deleting $path.." -foreground "magenta" 
            Remove-Item -Path "$path" -Force -Recurse -ErrorAction SilentlyContinue
            Write-Host " "
    
            return $true
        }
        catch {
    
            $_
            return $false
        }	
    }
    else {
    
        return $true
    }
}

function CreateIEmailTemplate($path) {

    $dest = "$path\src\app\ecomengine\dto"
    $fn = "$dest\iemail-template.ts"
    $successfullDelete = DeleteFile $fn

    CreateFolder $dest
    Set-Location $dest

    ng g interface IEmailTemplate --spec=false

    #------
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'export interface IEmailTemplate {
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
}'
    $code | Set-Content $fn

    Write-Host 
    Set-Location $path
}

function CreateEmailtemplateSearchRequest($path) {

    $dest = "$path\src\app\ecomengine\requests"
    $fn = "$dest\emailtemplate-search-request.ts"
    $successfullDelete = DeleteFile $fn

    CreateFolder $dest
    Set-Location $dest

    ng g class EmailtemplateSearchRequest --spec=false

    $successfullDelete = DeleteFile $fn

    #------
   
    $code = 
    'export class EmailtemplateSearchRequest {
    constructor(public search = "",
        public page = 0,
        public desc = false,
        public field = 0) { }
}'

    $code | Set-Content $fn

    Write-Host 
    Set-Location $path
}

function CreateEcomEngineService($path) {

    $dest = "$path\src\app\ecomengine\services"
    $fn = "$dest\ecomengine.service.ts"
    $successfullDelete = DeleteFile $fn

    CreateFolder $dest
    Set-Location $dest

    ng g service EcomEngine --spec=false

    #------
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'import { Injectable } from "@angular/core";
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

        const cleanedRequest = new EmailtemplateSearchRequest(request.search, request.page, request.desc, request.field);

        if (cleanedRequest.search === "") { cleanedRequest.search = undefined; }
        if (cleanedRequest.page > 1) { } else { cleanedRequest.page = undefined; }
        if (!cleanedRequest.desc) { cleanedRequest.desc = undefined; }
        if (cleanedRequest.desc) { cleanedRequest.desc = true } else { cleanedRequest.desc = undefined; }
        if (cleanedRequest.field > 0) { } else { cleanedRequest.field = undefined; }

        return cleanedRequest;
    }
}'
    $code | Set-Content $fn

    Write-Host 
    Set-Location $path
}

function CreateEcomEngineComponentCss($dest) {

    $fn = "$dest\ecomengine.component.css"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    '.home-header {
    text-align: center;
}

.home-header-image {
    max-width: 100%;
    height: auto;
    text-align: center;
}
.error-message {
    color: #fc2929 !important;
}

.error-message-container {
    text-align: center !important;
    font-family: "Roboto";
    color: #4E4444;
    font-size: large;
    font-weight: bolder;
}

.center {
    text-align: center !important;
}
'
    $code | Set-Content $fn

    Write-Host 
}

function CreateEcomEngineComponentHtml($dest) {

    $fn = "$dest\ecomengine.component.html"
    $successfullDelete = DeleteFile $fn
    $singleQoute = "'"
    $code = 
    '<div class="ui-g">
    <div class="ui-g-12 animated fadeIn">
        <div class="home-header">
            <img class="home-header-image" src="assets/img/homeheader.png">
        </div>
    </div>
</div>
<div class="profile header ui-g">
    <div class="ui-g-12 ui-md-8 ui-lg-9 ">
        <p-autoComplete [(ngModel)]="searchRequest.search" [suggestions]="searchSuggestions"
                        (completeMethod)="getSuggestions($event)"
                        placeholder="Find Email Templates here"
                        [disabled] ="isBusy"
                        [minLength]="1" [style]="{' + $singleQoute + 'width' + $singleQoute + ':' + $singleQoute + '100%' + $singleQoute + '}"
                        [inputStyle]="{' + $singleQoute + 'width' + $singleQoute + ':' + $singleQoute + '100%' + $singleQoute + '}"
                        class="p-autocomplete"></p-autoComplete>
    </div>
    <div class="ui-g-12 ui-md-4 ui-lg-3">
        <button pButton type="button" (click)="onSearchClicked()" icon="fa fa-search-plus" label="Search Now" [disabled]="isBusy"
                class="ui-g-12 ui-button-warning" item-width="100%"></button>
    </div>
</div>
<div class="ui-g">
    <div class="ui-g-12 ">
        <div *ngIf="!isBusy" class="error-message-container animated bounce rubberBand">
            <label *ngIf="!searchResponse.recordCount && !hasErrors">Opps.. hotel not found. Please try again.</label>
            <label *ngIf="hasErrors" class="error-message">Opps.. something went wrong. Please try again.</label>
        </div>
    </div>
</div>
<div class="ui-g">
    <div class="ui-g-12 ">
        <div *ngIf="!hasErrors" class="center animated fadeIn">
            <app-busy-indicator [isBusy]="isBusy" title="Searching.."></app-busy-indicator>
        </div>
    </div>
</div>
<div class="ui-g">
    <div class="ui-g-12">
        <div *ngIf="!hasErrors && !initialLoad" class="animated fadeIn">
            <p-table [columns]="cols" [value]="searchResponse.items" [lazy]="true" (onLazyLoad)="onLazyLoad($event)" [rowHover]="true"
                     [paginator]="canShowPager" [rows]="searchResponse.rowsPerPage" [totalRecords]="searchResponse.recordCount"
                     selectionMode="single" alwaysShowPaginator="false">
                <ng-template pTemplate="header" let-columns>
                    <tr>
                        <th *ngFor="let col of columns" [pSortableColumn]="col.field">
                            {{col.header}}
                            <p-sortIcon [field]="col.field"></p-sortIcon>
                        </th>
                    </tr>
                </ng-template>
                <ng-template pTemplate="body" let-rowData let-columns="columns">
                    <tr>
                        <td *ngFor="let col of columns">
                            <div *ngIf="col.field == ' + $singleQoute + 'dateUpdated' + $singleQoute + '">
                                {{rowData[col.field] | date: ' + $singleQoute + 'medium' + $singleQoute + '}}
                            </div>
                            <div *ngIf="col.field != ' + $singleQoute + 'dateUpdated' + $singleQoute + '">
                                {{rowData[col.field]}}
                            </div>
                        </td>
                    </tr>
                </ng-template>
                <ng-template pTemplate="summary">
                    {{searchResponse.recordCount | number : 0}} record(s) found.
                </ng-template>
            </p-table>
        </div>
    </div>
</div>'
    $code | Set-Content $fn

    Write-Host 
}

function CreateEcomEngineComponentTs($dest) {

    $fn = "$dest\ecomengine.component.ts"
    $successfullDelete = DeleteFile $fn
   
    $code = 
    'import { Component, OnInit, ChangeDetectorRef } from "@angular/core";
import { EcomengineService } from "./services/ecomengine.service";
import { EmailtemplateSearchRequest } from "./requests/emailtemplate-search-request";
import { IEmailTemplate } from "./dto/iemail-template";
import { SearchResponse } from "../shared/responses/search-response";

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

        return this.ecomengineService.search(this.searchRequest)
            .subscribe(r => {
                this.searchResponse = r;
                this.canShowPager = this.searchResponse.recordCount > this.searchResponse.rowsPerPage;

                if (!this.canShowPager) this.searchRequest.page = 1;

                const initial_load = this.initialLoad;
                this.initialLoad = false;
                this.isBusy = false;

                if (initial_load) this.cd.detectChanges(); //prevent ExpressionChangedAfterItHasBeenCheckedError

            }, error => this.handleError());
    }

    private handleError() {

        this.initialLoad = true;
        this.hasErrors = true;
        this.isBusy = false;
        this.cd.detectChanges();

    }

    getSuggestions(event): void {

        const query = event.query;

        this.ecomengineService.getSuggestions(query)
            .subscribe(suggestions => {
                this.searchSuggestions = suggestions;
            });
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

            this.searchRequest.field = this.cols.findIndex(c => event.sortField === c.field);
            this.searchRequest.desc = !(event.sortOrder === -1);

        }

        this.search();
    }
}'
    $code | Set-Content $fn

    Write-Host 
}

function CreateEcomEngineComponent($path) {

    $dest = "$path\src\app\ecomengine"
    $successfullDelete = DeleteDirectory $dest

    Set-Location "$path\src\app"
    ng g component Ecomengine --spec=false

    Set-Location $dest

    CreateEcomEngineComponentCss $dest
    CreateEcomEngineComponentHtml $dest
    CreateEcomEngineComponentTs $dest
}

function CreateEcomEngine($path) {

    CreateEcomEngineComponent $path
    CreateEcomEngineService $path
    CreateIEmailTemplate $path
    CreateEmailtemplateSearchRequest $path

}

function GetAppName() {

    return (get-item $PSScriptRoot).Name.Replace(".Spa","")
}

$projectPath = (get-item $PSScriptRoot)
$appName = GetAppName

CreateEcomEngine $projectPath
Set-Location $projectPath

# Don't forget to update angular.json and set:
#  styles
#  scripts
