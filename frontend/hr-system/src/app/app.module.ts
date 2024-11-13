import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AllProjectsComponent } from './components/projects/all-projects/all-projects.component';
import { ProjectCardComponent } from './components/projects/project-card/project-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginFormComponent } from './components/login/login-form/login-form.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import { ResetPasswordFormComponent } from './components/login/reset-password-form/reset-password-form.component';
import { SendHolidayRequestFormComponent } from './components/holidayRequests/send-holiday-request-form/send-holiday-request-form.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { EditProfileComponentComponent } from './components/employees/edit-profile-component/edit-profile-component.component';
import {MatTableModule} from '@angular/material/table';
import { EditEmployeeComponentComponent } from './components/employees/edit-employee-component/edit-employee-component.component'; 
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [
    AppComponent,
    AllProjectsComponent,
    ProjectCardComponent,
    NavbarComponent,
    LoginFormComponent,
    ResetPasswordFormComponent,
    SendHolidayRequestFormComponent,
    EditProfileComponentComponent,
    EditEmployeeComponentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatDatepickerModule,
    MatDialogModule,
    MatTableModule,
    MatSelectModule,
    FormsModule,
  ],
  providers: [provideNativeDateAdapter()],
  bootstrap: [AppComponent]
})
export class AppModule { }
