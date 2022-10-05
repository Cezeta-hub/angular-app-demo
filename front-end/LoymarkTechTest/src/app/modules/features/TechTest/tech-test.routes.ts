import { Routes } from "@angular/router";
import { NewUserPageComponent } from "./pages/Users/new-user-page/new-user-page.component";
import { UsersPageComponent } from "./pages/Users/users-page/users-page.component";

export const techTestRoutes: Routes = [
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full'
  },
  {
    path: 'users',
    data: {
      title: 'Users'
    },
    children: [
      {
        path: '',
        component: UsersPageComponent,
        pathMatch: 'full'
      },
      {
        path: 'new',
        component: NewUserPageComponent,
        data: {
          title: 'New User'
        }
      },

      {
        path: 'edit/:id',
        component: NewUserPageComponent,
        data: {
          title: 'User [id]'
        }
      }
    ]
  }
]
