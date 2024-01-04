import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { TokenService } from 'src/app/Services/token.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnDestroy {
  public registerForm: FormGroup = new FormGroup({});
  public errorMessage = '';

  private authServiceSubRef: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private tokenService: TokenService
  ) {
    this.createForm();
  }

  ngOnDestroy() {
    this.authServiceSubRef?.unsubscribe();
  }

  private createForm(): void {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  public registerUser(): void {
    if (this.authServiceSubRef) this.authServiceSubRef.unsubscribe();
    this.authServiceSubRef = this.authService.registerUser(this.registerForm.value).subscribe({
      next: (data: any) => {
        this.tokenService.SetToken(data.token);
        this.registerForm.reset();
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
