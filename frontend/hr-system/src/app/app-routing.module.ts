import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllProjectsComponent } from './components/projects/all-projects/all-projects.component';
import { EditProfileComponentComponent } from './components/employees/edit-profile-component/edit-profile-component.component';
import { AllClientsComponent } from './components/clients/all-clients/all-clients.component';
import { CreateClientComponent } from './components/clients/create-client/create-client.component';

const routes: Routes = [
  { path:'projects', component: AllProjectsComponent},
  //{ path:'employees', component: AllEmployeesComponent},
  { path:'clients', component: AllClientsComponent},
  //{ path:'holidayRequests', component: AllHolidayRequestsComponent},
  //{ path:'my-projects', component: MyProjectsComponent},
  { path:'profile', component: EditProfileComponentComponent},
  { path:'create-client', component:CreateClientComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
