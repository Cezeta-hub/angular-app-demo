import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
// import { AuthenticationService } from './globalServices/authenticacion.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private previousUrl: string;
  private currentUrl: string;
  // private auth: AuthenticationService,
  constructor(private router: Router){
  }
  ngOnInit(): void {
    // this.auth.CheckTokenInUrl();
    // this.auth.GetRedirectionUrl();
    // this.router.events.pipe(
    //     filter((event) => event instanceof NavigationEnd))
    //     .subscribe((event: any) => {
    //         this.previousUrl = this.currentUrl;
    //         this.currentUrl = event.url;
    //         this.auth.SetPreviousUrl(this.previousUrl);
    //     });
  }
}
