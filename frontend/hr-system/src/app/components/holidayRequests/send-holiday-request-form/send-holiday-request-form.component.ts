import { Component, EventEmitter, Output, ɵprovideZonelessChangeDetection } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { CreateHolidayRequestDto } from '../../../core/dtos/holiday-request/create-holiday-request.dto';
import { HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { JwtService } from '../../../core/services/jwt.service';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-send-holiday-request-form',
  templateUrl: './send-holiday-request-form.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './send-holiday-request-form.component.css'
})
export class SendHolidayRequestFormComponent {
  constructor(private jwtService: JwtService, private router: Router, private swal : AlertService, private readonly dialogRef: MatDialogRef<SendHolidayRequestFormComponent>, private holidayRequestService: HolidayRequestService){}
  readonly range = new FormGroup({
    start: new FormControl<Date | null | undefined>(null),
    end: new FormControl<Date | null | undefined>(null),
  });

  onCancel(): void {
    this.dialogRef.close();
  }

  private destroy$ = new Subject<void>();
  sendRequest(): void{
    const dto: CreateHolidayRequestDto = {
      start: this.range.value.start,
      end: this.range.value.end,
      senderId: this.jwtService.getIdFromToken(),
      status: HolidayRequestStatus.Pending
    }    
    this.holidayRequestService.createHolidayRequest(dto)
    .pipe(takeUntil(this.destroy$), tap((response) => {
      this.swal.fireSwalSuccess("Holiday request created successfully")
      this.router.navigate(['holiday-requests'])
      }),
      catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
      })).subscribe();
    this.onCancel();
  }
}
