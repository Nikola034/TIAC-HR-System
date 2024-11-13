import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
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
