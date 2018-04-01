import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

import { MenuModule } from 'primeng/menu';
import { TableModule } from 'primeng/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { SharedModule } from 'primeng/shared';



import { BusyIndicatorComponent } from './shared/busy-indicator/busy-indicator.component';
import { EcomengineService } from './ecomengine/ecomengine.service';
import { EcomengineComponent } from './ecomengine/ecomengine.component';

const appRoutes: Routes = [
    { path: '', redirectTo: '/ecomengine', pathMatch: 'full' },
    { path: 'ecomengine', component: EcomengineComponent }
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
        DropdownModule,
        AutoCompleteModule,
        TableModule,
        HttpModule,
        HttpClientModule,
        SharedModule
    ],
    providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }, EcomengineService],
    bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
    return 'https://localhost:44361/api/';
}




