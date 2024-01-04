import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { TokenService } from 'src/app/Services/token.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnDestroy {
  public loginForm: FormGroup = new FormGroup({});
  public errorMessage = '';

  private userServiceSubRef: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private tokenService: TokenService
  ) {
    this.createForm();
  }

  ngOnDestroy() {
    this.userServiceSubRef?.unsubscribe();
  }

  private createForm(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public loginUser(): void {
    if (this.userServiceSubRef) this.userServiceSubRef.unsubscribe();
    this.userServiceSubRef = this.authService.loginUser(this.loginForm.value).subscribe({
      next: (data: any) => {
        console.log('Logindata:', data);
        this.loginForm.reset();
        this.tokenService.SetToken(data.token);
        this.router.navigate(['']);
      },
      error: (err: any) => {
        console.log(err);
        if (err.error.message) {
          this.errorMessage = err.error.message;
        }
        if (err.error.msg) {
          this.errorMessage = err.error.msg;
        }
      },
    });
  }
}
