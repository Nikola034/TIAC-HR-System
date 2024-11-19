import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginDto } from '../../../core/dtos/account/login.dto';
import { AccountService } from '../../../core/services/account.service';
import { catchError, Observable, Subject, takeUntil, tap, throwError } from 'rxjs';
import { JwtService } from '../../../core/services/jwt.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  loginForm: FormGroup;
  private destroy$ = new Subject<void>()

  constructor(private fb: FormBuilder, private accountService : AccountService,
               private jwtService: JwtService, private router: Router, private swal : AlertService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const dto : LoginDto = {
        email : this.loginForm.get('email')?.value,
        password : this.loginForm.get('password')?.value
      };
      this.accountService.login(dto).pipe(takeUntil(this.destroy$), tap(
        response => {
          this.jwtService.setTokens(response)
          if(this.jwtService.IsLoggedIn()) {
            if(this.jwtService.IsDeveloper()){
              this.router.navigate(['my-projects'])
              return;
            }
            if(this.jwtService.IsManager()) 
              this.router.navigate(['projects'])
            else
              this.swal.fireSwalError("Invalid role found in token")
          }
          else
          {
            this.swal.fireSwalError("An error occured while reading token")
          }
        }
      ), catchError(
        (error: HttpErrorResponse): Observable<any> => {
            this.swal.fireSwalError(error.error.detail)
            return throwError(() => error);
        },
      )).subscribe()
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
