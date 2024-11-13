import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-employee-component',
  templateUrl: './edit-employee-component.component.html',
  styleUrl: './edit-employee-component.component.css',
})
export class EditEmployeeComponentComponent {
  editEmployeeForm: FormGroup;
  selectedRole = 'developer';

  constructor(private fb: FormBuilder) {
    this.editEmployeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      daysOff: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (this.editEmployeeForm.valid) {
      console.log('Form Submitted', this.editEmployeeForm.value);
    }
  }
}
