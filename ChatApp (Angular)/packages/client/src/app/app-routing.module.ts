import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Modules/auth/Guard/auth.guard';

const routes: Routes = [
  {
    // route: http://localhost:4200/auth
    path: 'auth',
    loadChildren: () => import('./Modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    loadChildren: () => import('./Modules/main/main.module').then((m) => m.MainModule),
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
