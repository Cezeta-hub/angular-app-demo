import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { filter, Observable, Subscriber, tap} from 'rxjs';
import { PaginatedResult } from 'src/app/globalInterfaces/paginated-result.interface';
import { QueryResult } from 'src/app/globalInterfaces/query-result.interface';
import { NotificationService } from 'src/app/globalServices/notification.service';
import { environment } from 'src/environments/environment';
import { History, HistorySearchPayload } from '../interfaces/History/history.interface';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }
  constructor(private notificationService: NotificationService, private http: HttpClient) { }

  public GetEntries(filters: HistorySearchPayload): Observable<QueryResult<PaginatedResult<History>>> {
    return this.http.get<QueryResult<PaginatedResult<History>>>(environment.API_TechTest + 'History', {params: {...filters}} ).pipe(
      tap({
        next: (data) => {},
        error: (e) => {
          this.notificationService.ShowError("There was an error while fetching History entries.");
        },
        complete: () => {}
      })
    )
  }

  // ------------------------------------//
  // GET OPTIONS SECTION (for dropdowns) //
  // ------------------------------------// 
}
