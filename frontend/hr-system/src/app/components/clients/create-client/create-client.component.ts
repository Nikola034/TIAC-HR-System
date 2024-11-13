import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ClientService } from '../../../core/services/client.service';
import { CreateClientDto } from '../../../core/dtos/client/create-client.dto';
import { Subject, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrl: './create-client.component.css'
})
export class CreateClientComponent {

  private destroy$ = new Subject<void>();
  createClientForm: FormGroup;

  constructor(private fb: FormBuilder, private clientService : ClientService) {
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
        
      })).subscribe();
      console.log('Form Submitted', this.createClientForm.value);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
