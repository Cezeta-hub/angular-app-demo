import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpParams, HttpRequest, HttpResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, Observable, tap, throwError } from "rxjs";
import { AuthenticationService } from "./globalServices/authenticacion.service";
import { HelperService } from "./globalServices/helper.service";

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    constructor(private helperService: HelperService, private router: Router, private authenticationService: AuthenticationService){}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // let httpParams: HttpParams = new HttpParams();
        // request.params.keys().forEach(x => {
        //     if(request.params.get(x) !== undefined && request.params.get(x) !== null) {
        //         let param = request.params.get(x);
        //         httpParams = httpParams.append(x, param as string);
        //     }
        // })
        // let cloneOptions: any = {
        //     params: httpParams
        // }
        // let token = window.localStorage.getItem("token");
        // if(token)
        //     cloneOptions = {
        //         ...cloneOptions,
        //         setHeaders: {Authorization: `Bearer ${token}`}
        //     }
        //     request = request.clone(cloneOptions);

        return next.handle(request).pipe(
            // catchError((response) => {
            //     if(response.status == HttpStatusCode.Unauthorized) {
            //         var rediretionUrl = response.headers.get("location");
            //         if(rediretionUrl) {
            //             this.authenticationService.SetRedirectionUrl(rediretionUrl);
            //             this.authenticationService.Redirect();
            //         }
            //         else {
            //             this.router.navigateByUrl('unauthorized', {state: {code: HttpStatusCode.Unauthorized}});
            //         }
            //     }
            //     else if (response.status == HttpStatusCode.Forbidden)
            //         this.router.navigateByUrl('unauthorized', {state: {code: HttpStatusCode.Forbidden}});
            //     this.helperService.FormatError(response);
            //     return throwError(() => response);
            // })
        )
    }
}