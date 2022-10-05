import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { filter, Observable, Subscriber, tap} from 'rxjs';
import { PaginatedResult } from 'src/app/globalInterfaces/paginated-result.interface';
import { QueryResult } from 'src/app/globalInterfaces/query-result.interface';
import { NotificationService } from 'src/app/globalServices/notification.service';
import { environment } from 'src/environments/environment';
import { CountryListResult, IdResult, User, UserPayload, UsersSearchPayload } from '../interfaces/Users/user.interface';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }
  constructor(private notificationService: NotificationService, private http: HttpClient) { }

  public GetUsers(filters: UsersSearchPayload): Observable<QueryResult<PaginatedResult<User>>> {
    return this.http.get<QueryResult<PaginatedResult<User>>>(environment.API_TechTest + 'Users', {params: {...filters}} ).pipe(
      tap({
        next: (data) => {},
        error: (e) => {
          this.notificationService.ShowError("There was an error while fetching the Users.");
        },
        complete: () => {}
      })
    )
  }
  public GetUserById(id: number): Observable<QueryResult<User>> {
    return this.http.get<QueryResult<User>>(environment.API_TechTest + 'Users/'+id).pipe(
      tap({
        error: (e) => {
          this.notificationService.ShowError("There was an error getting User #"+id+".");
        },
        complete: () => {}
      })
    )
  }

  public CreateUser(payload : UserPayload): Observable<IdResult> {
    return this.http.post<IdResult>(environment.API_TechTest + 'Users',  JSON.stringify({...payload}), this.httpOptions).pipe(
      tap({
        next: (data) => {this.notificationService.ShowSuccess("The User was successfully saved.")},
        error: (e) => {  this.notificationService.ShowError("There was an error creating the User."); },
      })
    );
  }
  public UpdateUser(payload : UserPayload): Observable<IdResult> {
    return this.http.put<IdResult>(environment.API_TechTest + 'Users',  JSON.stringify({...payload}), this.httpOptions).pipe(
      tap({
        next: (data) => {this.notificationService.ShowSuccess("The User was successfully saved.")},
        error: (e) => {  this.notificationService.ShowError("There was an error saving the User."); },
      })
    );
  }
  public DeleteUser(id: number): Observable<any> {
    return this.http.delete<any>(environment.API_TechTest + 'Users/'+id).pipe(
      tap({
        next: (d) => { this.notificationService.ShowSuccess("The User was successfully deleted.") },
        error: (e) => { this.notificationService.ShowError("There was an error deleting User #"+id+"." ) }
      })
    )
  }

  // ------------------------------------//
  // GET OPTIONS SECTION (for dropdowns) //
  // ------------------------------------//

  public GetCountries(): Observable<CountryListResult> {
    return this.http.get<CountryListResult>(environment.API_TechTest + 'Countries').pipe(
      tap({
        error: (e) => { this.notificationService.ShowError("Countries couldn't be fetched."); },
      })
    )
  }
  
}
