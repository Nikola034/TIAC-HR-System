import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ClientService } from '../../../core/services/client.service';
import { CreateClientDto } from '../../../core/dtos/client/create-client.dto';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrl: './create-client.component.css'
})
export class CreateClientComponent {

  private destroy$ = new Subject<void>();
  createClientForm: FormGroup;

  constructor(private fb: FormBuilder, private clientService : ClientService, private router : Router, private swal : AlertService) {
    this.createClientForm = this.fb.group({
      name: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.createClientForm.valid) {
      const dto: CreateClientDto = {
          name: this.createClientForm.get('name')?.value,
          country: this.createClientForm.get('country')?.value
        };
      this.clientService.createClient(dto)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.swal.fireSwalSuccess("Client created successfully")
          this.router.navigate(['clients'])
      }),
      catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
      })).subscribe();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
