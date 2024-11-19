import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil, tap, catchError, Observable, throwError } from 'rxjs';
import { LoginDto } from '../../../core/dtos/account/login.dto';
import { AccountService } from '../../../core/services/account.service';
import { AlertService } from '../../../core/services/alert.service';
import { JwtService } from '../../../core/services/jwt.service';
import { ResetPasswordDto } from '../../../core/dtos/account/reset-password.dto';

@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrl: './change-password-form.component.css'
})
export class ChangePasswordFormComponent {
  resetPasswordForm: FormGroup;
  private destroy$ = new Subject<void>()

  constructor(private fb: FormBuilder, private accountService : AccountService, private route: ActivatedRoute,
               private router: Router, private swal : AlertService) {
    this.resetPasswordForm = this.fb.group({
      password: ['', Validators.required],
      repeatedPassword: ['', [Validators.required,this.matchingPasswordsValidation()]],
    });
  }

  matchingPasswordsValidation(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let isValid = false
      if(this.resetPasswordForm)
        isValid = this.resetPasswordForm?.get('password')?.value == this.resetPasswordForm.get('repeatedPassword')?.value;
      return isValid ? null : { notMatching: 'Validation failed' };
    };
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const dto : ResetPasswordDto = {
        passwordResetToken : this.route.snapshot.paramMap.get('resetPasswordToken'),
        password : this.resetPasswordForm.get('password')?.value,

      };
      this.accountService.resetPassword(dto).pipe(takeUntil(this.destroy$), tap(
        response => {
          this.swal.fireSwalSuccess(response)
          this.router.navigate([''])
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
