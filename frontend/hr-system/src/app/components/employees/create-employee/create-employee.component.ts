import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrl: './create-employee.component.css'
})
export class CreateEmployeeComponent {
  createEmployeeForm: FormGroup;
  selectedRole = 'developer';

  constructor(private fb: FormBuilder) {
    this.createEmployeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      daysOff: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (this.createEmployeeForm.valid) {
      console.log('Form Submitted', this.createEmployeeForm.value);
    }
  }
}
