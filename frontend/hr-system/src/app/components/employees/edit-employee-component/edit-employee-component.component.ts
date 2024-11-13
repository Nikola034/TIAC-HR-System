import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-employee-component',
  templateUrl: './edit-employee-component.component.html',
  styleUrl: './edit-employee-component.component.css',
})
export class EditEmployeeComponentComponent {
  editEmployeeForm: FormGroup;
  selectedRole = 'developer';

  constructor(private fb: FormBuilder) {
    this.editEmployeeForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      role: [''],
    });
  }

  onSubmit() {
    if (this.editEmployeeForm.valid) {
      console.log('Form Submitted', this.editEmployeeForm.value);
    }
  }
}
