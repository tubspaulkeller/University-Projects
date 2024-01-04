import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from 'src/app/Components/auth/auth.component';
import { LoginComponent } from 'src/app/Components/login/login.component';
import { RegisterComponent } from 'src/app/Components/register/register.component';
import { SharedModule } from '../shared/shared.module';
import { AuthService } from 'src/app/Services/auth.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenService } from 'src/app/Services/token.service';
import { TokenInterceptorService } from 'src/app/Services/token-interceptor.service';

@NgModule({
  declarations: [AuthComponent, LoginComponent, RegisterComponent],
  imports: [AuthRoutingModule, SharedModule, HttpClientModule],
  providers: [
    AuthService,
    TokenService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
  ],
})
export class AuthModule {}
