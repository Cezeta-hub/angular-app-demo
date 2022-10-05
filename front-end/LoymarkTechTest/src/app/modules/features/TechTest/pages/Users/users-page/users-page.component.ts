import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { lastValueFrom } from 'rxjs';
import { SubSink } from 'subsink';
import { User, UsersSearchPayload } from '../../../interfaces/Users/user.interface';
import { UsersService } from '../../../services/user.service';

@Component({
  selector: 'users',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css'],
})
export class UsersPageComponent implements OnInit {
  private defaultFilters: any = {
    Name: '',
    OrderBy: "Name",
    Direction: 1,
    Page: 0,
    PageSize: 12,
  }
  public filters: any = { ...this.defaultFilters}
  public totalObjects: number = 0;
  public totalPages: number = 0;

  public users: User[];
  public loading: boolean = false;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private usersService: UsersService,
              private confirmationService: ConfirmationService
              ) { }

  ngOnInit(): void {
  }

  public async search(e?: any) {
    this.loading = true;
    if (e !== undefined){
      this.filters.Page = Math.floor(e.first/e.rows);
      this.filters.OrderBy = e.sortField;
      this.filters.Direction = e.sortOrder;
    }

    this.users = [];
    let payload: UsersSearchPayload = {...this.filters};
    let data = await lastValueFrom(this.usersService.GetUsers(payload));
    if (data) {
        this.totalObjects = data.Result.TotalObjects;
        this.totalPages = data.Result.TotalPages;
        this.users = [...data.Result.Result];
    } 
    this.loading = false;
  }

  public addNewUser (e:any) {
    let navigationExtras : NavigationExtras = {
      relativeTo: this.route,
    }
    this.router.navigate(['new'], navigationExtras);
  }
  public editUser (user: User) {
    let navigationExtras : NavigationExtras = {
      relativeTo: this.route,
    }
    this.router.navigate([`edit/${user.Id}`], navigationExtras);
  }
  public deleteUser (id: number){
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the User?',
      accept: async () => {
        let _ = await lastValueFrom(this.usersService.DeleteUser(id));
        this.search();
      }
    });
  }
  public resetFilters(): void {
    this.filters = {...this.defaultFilters};
    this.search();
  }
}