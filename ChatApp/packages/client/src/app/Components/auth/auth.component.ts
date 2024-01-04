import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent {
  public isLogin = true;

  changeForm(): void {
    this.isLogin = !this.isLogin;
  }
}
