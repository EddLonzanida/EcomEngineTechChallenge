import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/primeng';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    menuItems: MenuItem[];
    miniMenuItems: MenuItem[];

    ngOnInit() {
        this.menuItems = [
            { label: 'eComEngine', icon: 'fa fa-cogs', routerLink: ['/ecomengine'], routerLinkActiveOptions: { exact: true }},
        ];

        this.miniMenuItems = [];
        this.menuItems.forEach((item: MenuItem) => {
            const miniItem = { icon: item.icon, routerLink: item.routerLink, routerLinkActiveOptions: item.routerLinkActiveOptions };
            this.miniMenuItems.push(miniItem);
        });
    }
}
