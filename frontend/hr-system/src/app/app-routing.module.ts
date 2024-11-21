import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllProjectsComponent } from './components/projects/all-projects/all-projects.component';
import { AllClientsComponent } from './components/clients/all-clients/all-clients.component';
import { CreateClientComponent } from './components/clients/create-client/create-client.component';
import { EditProfileComponent } from './components/employees/edit-profile/edit-profile.component';
import { HolidayRequestsComponent } from './components/holidayRequests/holiday-requests-component/holiday-requests.component';
import { MyProjectsComponent } from './components/projects/my-projects/my-projects.component';
import { ProjectDetailsComponent } from './components/projects/project-details/project-details.component';
import { CreateProjectComponent } from './components/projects/create-project/create-project.component';
import { LoginFormComponent } from './components/login/login-form/login-form.component';
import { ResetPasswordFormComponent } from './components/login/reset-password-form/reset-password-form.component';
import { EmployeesComponent } from './components/employees/employees/employees.component';
import { CreateEmployeeComponent } from './components/employees/create-employee/create-employee.component';
import { EditEmployeeComponent } from './components/employees/edit-employee/edit-employee.component';
import { ChangePasswordFormComponent } from './components/login/change-password-form/change-password-form.component';
import { AuthGuardService } from './core/services/auth-guard.service';
import { AdminGuardService } from './core/services/admin-guard.service';
import { UserGuardService } from './core/services/user-guard.service';

const routes: Routes = [
  { path: 'projects', component: AllProjectsComponent,  canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'employees', component: EmployeesComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'create-employee', component: CreateEmployeeComponent, canActivate: [AuthGuardService, AdminGuardService] },
  { path: 'edit-employee', component: EditEmployeeComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'clients', component: AllClientsComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'profile', component: EditProfileComponent, canActivate: [AuthGuardService]},
  { path: 'create-client', component: CreateClientComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'edit-client', component: CreateClientComponent , canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'holiday-requests', component: HolidayRequestsComponent , canActivate: [AuthGuardService]},
  { path: 'my-projects', component: MyProjectsComponent, canActivate: [AuthGuardService, UserGuardService]},
  { path: 'my-projects/:id', component: ProjectDetailsComponent, canActivate: [AuthGuardService, UserGuardService] },
  { path: 'create-project', component: CreateProjectComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: 'edit-project', component: CreateProjectComponent, canActivate: [AuthGuardService, AdminGuardService]},
  { path: '', component: LoginFormComponent },
  { path: 'reset-password', component: ResetPasswordFormComponent },
  { path: 'passwordReset/:resetPasswordToken', component: ChangePasswordFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
