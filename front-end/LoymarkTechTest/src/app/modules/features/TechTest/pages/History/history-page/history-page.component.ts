import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { lastValueFrom } from 'rxjs';
import { SubSink } from 'subsink';
import { History, HistorySearchPayload } from '../../../interfaces/History/history.interface';
import { HistoryService } from '../../../services/history.service';

@Component({
  selector: 'history',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.css'],
})
export class HistoryPageComponent implements OnInit {
  public loading: boolean = false;
  
  private defaultFilters: any = {
    UserId: null,
    OrderBy: "ChangeDate",
    Direction: -1,
    Page: 0,
    PageSize: 12,
  }
  public filters: any = { ...this.defaultFilters}
  public totalObjects: number = 0;
  public totalPages: number = 0;

  public historyEntries: History[];

  constructor(private router: Router,
              private route: ActivatedRoute,
              private historyService: HistoryService,
              private confirmationService: ConfirmationService
              ) { }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get("id");
    if (id) {
      this.filters.UserId = parseInt(id);
    }
  }

  public search(e?: any) {
    this.loading = true;

    if (e !== undefined) {
      this.filters.Page = Math.floor(e.first/e.rows);
      this.filters.OrderBy = e.sortField;
      this.filters.Direction = e.sortOrder;
    }

    this.historyEntries = [];
    let payload: HistorySearchPayload = {...this.filters};
    if (!payload.UserId) payload.UserId = '';
    this.historyService.GetEntries(payload).toPromise().then((data) => {
      if (data) {
        this.totalObjects = data.Result.TotalObjects;
        this.totalPages = data.Result.TotalPages;
        this.historyEntries = [...data.Result.Result];
      }
    }).finally(() => this.loading = false);
  }

  public resetFilters(): void {
    this.filters = {...this.defaultFilters};
    this.search();
  }
}