import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginFormComponent } from './components/login/login-form/login-form.component';
import { ResetPasswordFormComponent } from './components/login/reset-password-form/reset-password-form.component';
import { EditProfileFormComponent } from './components/employees/edit-profile-form/edit-profile-form.component';
import { ProjectCardComponent } from './components/projects/project-card/project-card.component';
import { Project } from './model/entities/Project';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginFormComponent, ResetPasswordFormComponent, EditProfileFormComponent, ProjectCardComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  projects: Project[] = [
    {
      id: "1123143",
      title: 'Project Alpha',
      description: 'This is the first project.',
      client: { id: "1", country: 'USA', name: "Tiac" },
      teamLeadId: "addfafd"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    },
    {
      id: "123129999",
      title: 'Project Beta',
      description: 'This is the second project.',
      client: { id: "2", country: 'Germany', name: "Germ" },
      teamLeadId: "123213"
    }
  ];
}
