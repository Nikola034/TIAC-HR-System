import { Component, Input } from '@angular/core';
import { Project } from '../../../core/models/project.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrl: './project-card.component.css'
})
export class ProjectCardComponent {
  @Input() project!: Project;

  constructor(private router: Router) {}

  navigateToDetails() {
    this.router.navigate(['my-projects', this.project.id]);
  }
}
