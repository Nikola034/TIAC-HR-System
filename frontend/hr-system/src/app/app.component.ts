import { LoginFormComponent } from './components/login/login-form/login-form.component';
import { ResetPasswordFormComponent } from './components/login/reset-password-form/reset-password-form.component';
import { EditProfileFormComponent } from './components/employees/edit-profile-form/edit-profile-form.component';
import { ProjectCardComponent } from './components/projects/project-card/project-card.component';
import { Project } from './model/entities/Project';
import { SendHolidayRequestFormComponent } from './components/holidayrequests/send-holiday-request-form/send-holiday-request-form.component';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { JwtModule } from "@auth0/angular-jwt";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    CommonModule,
    JwtModule,
    LoginFormComponent,
    ResetPasswordFormComponent,
    EditProfileFormComponent,
    ProjectCardComponent,
    CommonModule,
    SendHolidayRequestFormComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  showNavbar = true;
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

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        // List of routes that should not show the navbar
        const excludedRoutes = ['/login', '/register'];

        // Check if the current route is in the list of excluded routes
        this.showNavbar = !excludedRoutes.includes(event.urlAfterRedirects);
      }
    });
  }
}
