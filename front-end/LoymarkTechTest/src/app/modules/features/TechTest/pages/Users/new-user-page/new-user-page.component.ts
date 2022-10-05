import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RoutesRecognized } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { lastValueFrom } from 'rxjs';
import { NotificationService } from 'src/app/globalServices/notification.service';
import { SubSink } from 'subsink';
import { User, UserPayload } from '../../../interfaces/Users/user.interface';
import { UsersService } from '../../../services/user.service';

@Component({
  selector: 'new-user-page',
  templateUrl: './new-user-page.component.html',
  styleUrls: ['./new-user-page.component.css'],
  providers: [ DialogService ],
})
export class NewUserPageComponent implements OnInit, OnDestroy {
    public editMode: boolean = false;
    public user: User = {} as User;

    public loading: boolean = false;
    public saving: boolean = false;

    public countryOptions: any[] = [];
    public selectedCountry: number = 0;
    public wishesContact: boolean = true;

    public form: FormGroup;
    private get name() { return this.form.get('name')};
    private get surname() { return this.form.get('surname')};
    private get email() { return this.form.get('email')};
    private get birthday() { return this.form.get('birthday')};
    private get telephone() { return this.form.get('telephone')};

    private subscriptions = new SubSink();
    constructor(private router: Router,
                private route: ActivatedRoute,
                private notificationService: NotificationService,
                public confirmationService: ConfirmationService,
                private usersService : UsersService)
    { }

    ngOnInit(): void {
        let id = this.route.snapshot.paramMap.get("id")
        if(id) {
            this.editMode = true;
            this.user.Id = parseInt(id);
            this.getUser(this.user.Id);
        } else {
            this.editMode = false;
            this.user = {
                Id : 0,
                Name: '',
                Surname: '',
                Birthday: '',
                Email: '',
                Telephone: undefined,
                CountryId: undefined,
                WishesToBeContacted: true,
                ChangeHistory: []
            }
        }

        this.form = new FormGroup({
            'name' : new FormControl(this.user.Name, Validators.required),
            'surname' : new FormControl(this.user.Surname, Validators.required),
            'email' : new FormControl(this.user.Email, Validators.required),
            'birthday' : new FormControl(this.user.Birthday, Validators.required),
            'telephone' : new FormControl(this.user.Telephone),
        });

        this.getCountryOptions();
    }

    ngOnDestroy(): void { this.subscriptions.unsubscribe(); }

    private async getUser(id: number) {
        this.loading = true;
        let data = await lastValueFrom(this.usersService.GetUserById(id));
        if (data) {
            this.user = data.Result;
            this.form.setValue({
                name: this.user.Name,
                surname: this.user.Surname,
                email: this.user.Email,
                birthday: new Date(this.user.Birthday),
                telephone: this.user.Telephone,
            });
            if (this.countryOptions.length > 0) 
                this.selectedCountry = this.user.Country?.Id ? this.user?.Country.Id : 0;
            this.wishesContact = this.user.WishesToBeContacted;
        }
        this.loading = false;
    }
    private async getCountryOptions() {
        let data = await lastValueFrom(this.usersService.GetCountries());
        if (data) { 
            this.countryOptions = data.Result;
            if (this.user.Country?.Id)
                this.selectedCountry = this.user.Country.Id;
        }
    }

    public async onSave (e:any) {
        debugger;
        this.saving = true;
        let payload = {
            Id: this.editMode ? this.user.Id : undefined,
            Name: this.name?.value,
            Surname: this.surname?.value,
            Birthday: this.birthday?.value,
            Email: this.email?.value,
            Telephone: this.telephone?.value,
            CountryCode: this.countryOptions.find(c => c.Id === this.selectedCountry).Code,
            WishesToBeContacted: this.wishesContact
        } as UserPayload;

        if (this.editMode) { // Update User
            let _ = await lastValueFrom(this.usersService.UpdateUser(payload));
        } else {             // Create User
            let _ = await lastValueFrom(this.usersService.CreateUser(payload));
        }
        this.saving = false;
        this.router.navigateByUrl('/tech-test/users');
    }

    public onCancel () {
        if(this.form.dirty)
            this.confirmationService.confirm({
                message: 'Are you sure you want to go back? All unsaved changes will be lost.',
                accept: () => {
                    this.router.navigateByUrl('/tech-test/users');
                }
            });
        else
            this.router.navigateByUrl('/tech-test/users');
    }

    public canSave() {
        return this.form.valid && this.selectedCountry !== null && this.selectedCountry !== 0;
    }
}
