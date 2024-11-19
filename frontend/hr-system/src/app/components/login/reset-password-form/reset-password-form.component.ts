import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../core/services/account.service';
import { AlertService } from '../../../core/services/alert.service';
import { catchError, Observable, Subject, takeUntil, tap, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrl: './reset-password-form.component.css'
})
export class ResetPasswordFormComponent {
  resetPasswordForm: FormGroup;
  private destroy$ = new Subject<void>()

  constructor(private fb: FormBuilder, private accountService : AccountService, private swal : AlertService) {
    this.resetPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      this.accountService.sendPasswordResetEmail(this.resetPasswordForm.get('email')?.value).pipe(takeUntil(this.destroy$),
      tap(
        response => {
          this.swal.fireSwalSuccess(response)
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
