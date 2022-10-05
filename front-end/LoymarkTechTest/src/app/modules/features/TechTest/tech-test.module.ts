import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { techTestRoutes } from './tech-test.routes';
import { SharedModule } from '../../shared/shared.module';
import { TabViewModule } from 'primeng/tabview';
import { UsersPageComponent } from './pages/Users/users-page/users-page.component';
import { NewUserPageComponent } from './pages/Users/new-user-page/new-user-page.component';
import { HistoryPageComponent } from './pages/History/history-page/history-page.component';

@NgModule({
  declarations: [
    UsersPageComponent,
    NewUserPageComponent,
    HistoryPageComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(techTestRoutes),
    TabViewModule
  ],
  entryComponents: [
  ]
})
export class TechTestModule { }
