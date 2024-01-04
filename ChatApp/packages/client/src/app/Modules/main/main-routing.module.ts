import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from '../../Components/users/users.component';
import { GroupsComponent } from '../../Components/groups/groups.component';
import { MainComponent } from '../../Components/main/main.component';
import { ProfilePictureComponent } from 'src/app/Components/profile-picture/profile-picture.component';
import { PrivateRoomComponent } from 'src/app/Components/private-room/private-room.component';
import { NormalRoomComponent } from 'src/app/Components/normal-room/normal-room.component';
import { MainRoomComponent } from '../../Components/main-room/main-room.component';

const routes: Routes = [
  // the main component acts as a wrapper to display sidenav / toolbar
  {
    path: '',
    component: MainComponent,
    children: [
      // route: http://localhost:4200 defaults to HomeComponent
      {
        path: '',
        component: MainRoomComponent,
      },
      // route: http://localhost:4200/users
      {
        path: 'users',
        component: UsersComponent,
      },
      {
        path: 'users/:name/:id',
        component: PrivateRoomComponent,
      },
      // route: http://localhost:4200/groups
      {
        path: 'groups',
        component: GroupsComponent,
      },
      {
        path: 'room/:id',
        component: NormalRoomComponent,
      },
      {
        path: 'profilepicture',
        component: ProfilePictureComponent,
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
    //canActivate: [AuthGuard],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule {}
