import { Component, OnInit } from '@angular/core';
// import { AuthenticationService } from 'src/app/globalServices/authenticacion.service';
import { MenuItem } from 'primeng/api';
@Component({
  selector: 'nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  // private authenticationService: AuthenticationService
  constructor() { }
  public username: string;
  public visibleSidebar1: boolean = false;
  public menu: MenuItem[];

  ngOnInit(): void {
    // this.username = this.authenticationService.GetUsername();
    this.username = "Admin";
    this.menu = [
      {
        label: 'Users', icon: 'pi pi-fw pi-users',
        routerLink: ['/tech-test/users'],
        routerLinkActiveOptions: 'active',
        command: () => {this.closeDrawer()}
      },
      {
          label: 'History', icon: 'pi pi-fw pi-building',
          routerLink: ['/tech-test/history'],
          routerLinkActiveOptions: 'active',
          command: () => {this.closeDrawer()}
      }
    ];
  }
  public changeDrawer(): void {
    this.visibleSidebar1 = !this.visibleSidebar1;
  }
  private closeDrawer(): void {
    if(this.visibleSidebar1)
      this.visibleSidebar1 = false;
  }
}
