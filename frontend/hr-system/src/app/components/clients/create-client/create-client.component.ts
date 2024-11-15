import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ClientService } from '../../../core/services/client.service';
import { CreateClientDto } from '../../../core/dtos/client/create-client.dto';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { Client } from '../../../core/models/client.model';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrl: './create-client.component.css'
})
export class CreateClientComponent {

  private destroy$ = new Subject<void>();
  createClientForm: FormGroup;
  isEditMode: boolean = false;
  existingClient !: Client;
  buttonContent : string ='Create Client';

  constructor(private fb: FormBuilder, private clientService : ClientService, private router : Router, private swal : AlertService) {
    this.createClientForm = this.fb.group({
      name: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Check if client data is passed via state
    this.existingClient = history.state.clientData;

    if (this.existingClient) {
      this.isEditMode = true;
      this.buttonContent = 'Update Client'
      this.createClientForm.patchValue({
        name: this.existingClient.name,
        country: this.existingClient.country
      });
    }
  }

  onSubmit() {
    if (this.createClientForm.valid) {
      if(!this.isEditMode){
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
          this.swal.fireSwalError("Something went wrong while creating client")
          return throwError(() => error);
        })).subscribe();
      }
      else {
        this.existingClient.name = this.createClientForm.get('name')?.value,
        this.existingClient.country = this.createClientForm.get('country')?.value
        this.clientService.updateClient(this.existingClient)
          .pipe(takeUntil(this.destroy$), tap((response) => {
            this.swal.fireSwalSuccess("Client update successfully")
            this.router.navigate(['clients'])
        }),
        catchError( error => {
          this.swal.fireSwalError("Something went wrong while updating client")
          return throwError(() => error);
        })).subscribe();
      }
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
