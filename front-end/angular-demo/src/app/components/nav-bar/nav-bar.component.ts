import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  
  public username: string;
  public isSidebarVisible: boolean = false;
  public menu: MenuItem[];

  private onSelectMenuOption = () => {this.closeDrawer()};

  constructor() { }

  ngOnInit(): void {
    this.username = "Admin";
    this.menu = [
      {
        label: 'About', icon: 'pi pi-home',
        routerLink: ['/tech-test/about'],
        routerLinkActiveOptions: 'active',
        command: this.onSelectMenuOption
      },
      {
        label: 'Users', icon: 'pi pi-fw pi-users',
        routerLink: ['/tech-test/users'],
        routerLinkActiveOptions: 'active',
        command: this.onSelectMenuOption
      },
      {
        label: 'History', icon: 'pi pi-fw pi-building',
        routerLink: ['/tech-test/history'],
        routerLinkActiveOptions: 'active',
        command: this.onSelectMenuOption
      }
    ];
  }
  
  public changeDrawer(): void {
    this.isSidebarVisible = !this.isSidebarVisible;
  }

  private closeDrawer(): void {
    if(this.isSidebarVisible)
      this.isSidebarVisible = false;
  }
}
