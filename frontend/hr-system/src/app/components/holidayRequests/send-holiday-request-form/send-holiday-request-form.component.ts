import { Component, EventEmitter, Output, ɵprovideZonelessChangeDetection } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { CreateHolidayRequestDto } from '../../../core/dtos/holiday-request/create-holiday-request.dto';
import { HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { JwtService } from '../../../core/services/jwt.service';

@Component({
  selector: 'app-send-holiday-request-form',
  templateUrl: './send-holiday-request-form.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './send-holiday-request-form.component.css'
})
export class SendHolidayRequestFormComponent {
  constructor(private jwtService: JwtService, private readonly dialogRef: MatDialogRef<SendHolidayRequestFormComponent>, private holidayRequestService: HolidayRequestService){}
  readonly range = new FormGroup({
    start: new FormControl<Date | null | undefined>(null),
    end: new FormControl<Date | null | undefined>(null),
  });

  onCancel(): void {
    this.dialogRef.close();
  }


  sendRequest(): void{
    const dto: CreateHolidayRequestDto = {
      start: this.range.value.start,
      end: this.range.value.end,
      senderId: this.jwtService.getIdFromToken(),
      status: HolidayRequestStatus.Pending
    }    
    console.log(dto)
    this.holidayRequestService.createHolidayRequest(dto).subscribe()
    this.onCancel();
  }
}
