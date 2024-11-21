import { Component, EventEmitter, Inject, Output, ɵprovideZonelessChangeDetection } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { CreateHolidayRequestDto } from '../../../core/dtos/holiday-request/create-holiday-request.dto';
import { HolidayRequest, HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { JwtService } from '../../../core/services/jwt.service';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-send-holiday-request-form',
  templateUrl: './send-holiday-request-form.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './send-holiday-request-form.component.css'
})
export class SendHolidayRequestFormComponent {
  constructor(private jwtService: JwtService, private router: Router, private employeeService: EmployeeService, private swal : AlertService, private readonly dialogRef: MatDialogRef<SendHolidayRequestFormComponent>, private holidayRequestService: HolidayRequestService){}
  readonly range = new FormGroup({
    start: new FormControl<Date | null | undefined>(null),
    end: new FormControl<Date | null | undefined>(null),
  });
  daysOff: number | undefined

  ngOnInit(){
    this.employeeService.getEmployeeById(this.jwtService.getIdFromToken())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.daysOff = response.daysOff
      })).subscribe();
  }

  onCancel(newRequest: HolidayRequest | undefined): void {
    this.dialogRef.close(newRequest);
  }

  rangeFilter(date: Date): boolean {
    const today = new Date();
    
    const isFutureDate = date.getTime() > today.getTime();
  
    const isWeekend = date.getDay() === 0 || date.getDay() === 6;

    const isNationalHoliday = (date.getDate() === 19 && date.getMonth() == 11) || (date.getDate() === 11 && date.getMonth() == 10) || (date.getDate() === 31 && date.getMonth() == 11)
    || (date.getDate() === 1 && date.getMonth() == 0) || (date.getDate() === 7 && date.getMonth() == 0) || (date.getDate() === 14 && date.getMonth() == 0)
  
    return isFutureDate && !isWeekend;
  }

  private destroy$ = new Subject<void>();
  sendRequest(): void{
    let start = this.range.value.start
    start?.setHours(start.getHours() + 1)
    let end = this.range.value.end
    end?.setHours(end.getHours() + 1)
    const dto: CreateHolidayRequestDto = {
      start: start,
      end: end,
      senderId: this.jwtService.getIdFromToken(),
      status: HolidayRequestStatus.Pending
    }    
    this.holidayRequestService.createHolidayRequest(dto)
    .pipe(takeUntil(this.destroy$), tap((response) => {
      this.swal.fireSwalSuccess("Holiday request created successfully")
      this.router.navigate(['holiday-requests'])
      this.onCancel(response);
    }),
      catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
      })).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
