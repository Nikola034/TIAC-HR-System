import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllProjectsComponent } from './components/projects/all-projects/all-projects.component';
import { AllClientsComponent } from './components/clients/all-clients/all-clients.component';
import { CreateClientComponent } from './components/clients/create-client/create-client.component';
import { EditProfileComponent } from './components/employees/edit-profile/edit-profile.component';
import { HolidayRequestsComponent } from './components/holidayRequests/holiday-requests-component/holiday-requests.component';

const routes: Routes = [
  { path:'projects', component: AllProjectsComponent},
  //{ path:'employees', component: AllEmployeesComponent},
  { path:'clients', component: AllClientsComponent},
  //{ path:'holidayRequests', component: AllHolidayRequestsComponent},
  //{ path:'my-projects', component: MyProjectsComponent},
  { path:'profile', component: EditProfileComponent},
  { path:'create-client', component:CreateClientComponent},
  //{ path:'clients', component: AllClientsComponent},
  { path:'holidayRequests', component: HolidayRequestsComponent},
  //{ path:'my-projects', component: MyProjectsComponent},
  { path:'profile', component: EditProfileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
