import { Component } from '@angular/core';
import { Project } from '../../../core/models/project.model';
import { ProjectService } from '../../../core/services/project.service';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { JwtService } from '../../../core/services/jwt.service';
import { Subject, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html',
  styleUrl: './my-projects.component.css'
})
export class MyProjectsComponent {
  projects : Project[] = []
  private destroy$ = new Subject<void>();

  constructor(private jwtService: JwtService, private projectService: ProjectService, private router: Router, private swal: AlertService){}

  ngOnInit(){
    this.projectService.getAllForEmployee(this.jwtService.getIdFromToken())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.projects = response.projects
      })).subscribe();
  }
}
