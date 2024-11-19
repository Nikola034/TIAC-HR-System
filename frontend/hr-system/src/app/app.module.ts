import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AllProjectsComponent } from './components/projects/all-projects/all-projects.component';
import { ProjectCardComponent } from './components/projects/project-card/project-card.component';
import {MatCardActions, MatCardModule} from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginFormComponent } from './components/login/login-form/login-form.component';
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import { ResetPasswordFormComponent } from './components/login/reset-password-form/reset-password-form.component';
import { SendHolidayRequestFormComponent } from './components/holidayRequests/send-holiday-request-form/send-holiday-request-form.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AllClientsComponent } from './components/clients/all-clients/all-clients.component';
import { PaginatorComponent } from './components/shared/paginator/paginator.component';
import { CreateClientComponent } from './components/clients/create-client/create-client.component';
import { HolidayRequestsComponent } from './components/holidayRequests/holiday-requests-component/holiday-requests.component';
import { EditProfileComponent } from './components/employees/edit-profile/edit-profile.component';
import {MatTableModule} from '@angular/material/table';
import { EditEmployeeComponent } from './components/employees/edit-employee/edit-employee.component'; 
import { MatSelectModule } from '@angular/material/select';
import { CreateEmployeeComponent } from './components/employees/create-employee/create-employee.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyProjectsComponent } from './components/projects/my-projects/my-projects.component';
import { ProjectDetailsComponent } from './components/projects/project-details/project-details.component';
import {MatDividerModule} from '@angular/material/divider';
import {MatTooltipModule} from '@angular/material/tooltip';
import { CreateProjectComponent } from './components/projects/create-project/create-project.component';
import { HttpInterceptorService } from './core/services/http-interceptor.service';
import { EmployeesComponent } from './components/employees/employees/employees.component';
import { ClientProjectsComponent } from './components/clients/client-projects/client-projects.component';
import { JWT_OPTIONS, JwtInterceptor, JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';

// Function to retrieve the token
export function tokenGetter() {
  return localStorage.getItem('access_token');
}
import { ChangePasswordFormComponent } from './components/login/change-password-form/change-password-form.component';

@NgModule({
  declarations: [
    AppComponent,
    AllProjectsComponent,
    ProjectCardComponent,
    NavbarComponent,
    LoginFormComponent,
    ResetPasswordFormComponent,
    SendHolidayRequestFormComponent,
    AllClientsComponent,
    PaginatorComponent,
    CreateClientComponent,
    EditProfileComponent,
    HolidayRequestsComponent,
    EditEmployeeComponent,
    CreateEmployeeComponent,
    MyProjectsComponent,
    ProjectDetailsComponent,
    CreateProjectComponent,
    EmployeesComponent,
    ClientProjectsComponent,
    ChangePasswordFormComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
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
    HttpClientModule,
    MatError,
    MatCardActions,
    MatSelectModule,
    FormsModule,
    MatDividerModule,
    MatTooltipModule
  ],
  providers: [provideNativeDateAdapter(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true,
    },
    // Other providers...
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
