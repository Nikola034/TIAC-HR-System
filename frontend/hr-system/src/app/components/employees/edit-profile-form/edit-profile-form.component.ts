import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'edit-profile-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    MatIconModule
  ],
  templateUrl: './edit-profile-form.component.html',
  styleUrl: './edit-profile-form.component.css'
})
export class EditProfileFormComponent {
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
