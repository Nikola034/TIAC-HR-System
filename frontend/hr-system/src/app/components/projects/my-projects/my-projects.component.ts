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

  noProjects: boolean | undefined

  constructor(private jwtService: JwtService, private projectService: ProjectService, private router: Router, private swal: AlertService){}

  ngOnInit(){
    this.projectService.getAllForEmployee(this.jwtService.getIdFromToken())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.projects = response.projects
        if(this.projects.length == 0){
          this.noProjects = true
        }
      })).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
