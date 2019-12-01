import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule, Routes } from "@angular/router";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppComponent } from "./app.component";
// import { MenuModule } from "primeng/menu";
import { TableModule } from "primeng/table";
// import { AutoCompleteModule } from "primeng/autocomplete";
// import { ButtonModule } from "primeng/button";
// import { SharedModule } from "primeng/shared";

import {
    MenuModule,
    ButtonModule,
    AutoCompleteModule,
    SharedModule
  } from 'primeng/primeng';


import { SearchService } from "./shared/services/search.service";
import { BusyIndicatorComponent } from "./shared/busy-indicator/busy-indicator.component";
import { EcomengineService } from "./ecomengine/services/ecomengine.service";
import { EcomengineComponent } from "./ecomengine/ecomengine.component";

const appRoutes: Routes = [
    { path: "", redirectTo: "/ecomengine", pathMatch: "full" },
    { path: "ecomengine", component: EcomengineComponent }
];

@NgModule({
    declarations: [
        AppComponent,
        BusyIndicatorComponent,
        EcomengineComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot(appRoutes),
        BrowserAnimationsModule,
        MenuModule,
        ButtonModule,
        AutoCompleteModule,
        TableModule,
        HttpClientModule,
        SharedModule
    ],
    providers: [{ provide: "BASE_URL", useFactory: getBaseUrl }, SearchService, EcomengineService],
    bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
    return "https://localhost:44377/api/";
}




