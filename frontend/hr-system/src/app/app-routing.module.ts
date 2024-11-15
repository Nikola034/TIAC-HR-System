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

const routes: Routes = [
  { path:'projects', component: AllProjectsComponent},
  { path:'employees', component: EmployeesComponent},
  { path:'clients', component: AllClientsComponent},
  { path:'profile', component: EditProfileComponent},
  { path:'create-client', component:CreateClientComponent},
  { path:'holiday-requests', component: HolidayRequestsComponent},
  { path:'my-projects', component: MyProjectsComponent},
  { path:'my-projects/:id', component: ProjectDetailsComponent},
  { path:'profile', component: EditProfileComponent},
  { path: 'create-project', component: CreateProjectComponent},
  { path: '', component: LoginFormComponent},
  { path: 'reset-password', component: ResetPasswordFormComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
