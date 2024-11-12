import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-profile-component',
  templateUrl: './edit-profile-component.component.html',
  styleUrl: './edit-profile-component.component.css'
})
export class EditProfileComponentComponent {
  editProfileForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.editProfileForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.editProfileForm.valid) {
      console.log('Form Submitted', this.editProfileForm.value);
    }
  }

}
