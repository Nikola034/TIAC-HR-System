import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-send-holiday-request-form',
  templateUrl: './send-holiday-request-form.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './send-holiday-request-form.component.css'
})
export class SendHolidayRequestFormComponent {

  constructor(private readonly dialogRef: MatDialogRef<SendHolidayRequestFormComponent>){}
  readonly range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  onCancel(): void {
    this.dialogRef.close();
  }
  sendRequest(): void{
    
  }

}
