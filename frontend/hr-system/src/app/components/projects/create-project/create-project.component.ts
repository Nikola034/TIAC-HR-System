import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject, takeUntil, tap } from 'rxjs';
import { ProjectService } from '../../../core/services/project.service';
import { CreateProjectDto } from '../../../core/dtos/project/create-project.dto';
import { Client } from '../../../core/models/client.model';
import { Employee } from '../../../core/models/employee.model';
import { ClientService } from '../../../core/services/client.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { ClientWithNumberOfProjects } from '../../../core/dtos/client/client-with-number-of-projects.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrl: './create-project.component.css'
})
export class CreateProjectComponent {

  private destroy$ = new Subject<void>();
  createProjectForm: FormGroup;

  constructor(private fb: FormBuilder, private projectService : ProjectService,
              private clientService : ClientService, private employeeService : EmployeeService, private router: Router) {
    this.createProjectForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      client:['', Validators.required],
      teamLead:[''],
    });
  }

  

  clients : ClientWithNumberOfProjects[] = []
  developers : Employee[] = []

  ngOnInit(){
    this.clientService.getAllClients("?page=1&items-per-page=100")
      .pipe(takeUntil(this.destroy$),tap( response => {
        this.clients = response.clients;
      })).subscribe()

      this.employeeService.getAllDevelopers()
      .pipe(takeUntil(this.destroy$),tap( response => {
        this.developers = response.developers;
      })).subscribe()
  }

  onSubmit() {
    if (this.createProjectForm.valid) {
      const dto: CreateProjectDto = {
          title: this.createProjectForm.get('title')?.value,
          description: this.createProjectForm.get('description')?.value,
          clientId: this.createProjectForm.get('client')?.value,
          teamLeadId: this.createProjectForm.get('teamLead')?.value,
        };
      this.projectService.createProject(dto)
        .pipe(takeUntil(this.destroy$), tap((response) => {
        if(response)
            this.router.navigate(['projects'])
      })).subscribe();
      console.log('Form Submitted', this.createProjectForm.value);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
