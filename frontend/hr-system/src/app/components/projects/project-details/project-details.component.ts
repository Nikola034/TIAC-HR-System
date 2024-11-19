import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from '../../../core/models/project.model';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { ProjectService } from '../../../core/services/project.service';
import { catchError, forkJoin, map, Observable, of, Subject, switchMap, takeUntil, tap, throwError } from 'rxjs';
import { EmployeeService } from '../../../core/services/employee.service';
import { AlertService } from '../../../core/services/alert.service';
import { EmployeeForProjectDto } from '../../../core/dtos/employee/employee-for-project.dto';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrl: './project-details.component.css',
})
export class ProjectDetailsComponent implements OnInit {
  projectId!: string;
  project: Project | any;
  employees: EmployeeForProjectDto[] = [];
  teamLead: EmployeeForProjectDto | undefined
  private destroy$ = new Subject<void>();

  constructor(private route: ActivatedRoute, private projectService: ProjectService, private employeeService: EmployeeService, private router: Router, private swal: AlertService) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id')!;
    this.loadProject();
  }

  getTeamLead() : string{
    return this.teamLead?.name + ' ' + this.teamLead?.surname
  }

  loadProject(): void{
    this.projectService.getProjectById(this.projectId)
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.project = response
        this.employees = response.working
        this.teamLead = response.working.find(x => x.id == response.teamLeadId) || {id: '', name: '/', surname: ''}
      })).subscribe();
  }

  goBack(): void {
    this.router.navigate(['/my-projects']);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
