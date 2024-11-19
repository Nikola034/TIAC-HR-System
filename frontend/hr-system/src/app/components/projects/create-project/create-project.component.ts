import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { ProjectService } from '../../../core/services/project.service';
import { CreateProjectDto } from '../../../core/dtos/project/create-project.dto';
import { Employee } from '../../../core/models/employee.model';
import { ClientService } from '../../../core/services/client.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { ClientWithNumberOfProjects } from '../../../core/dtos/client/client-with-number-of-projects.dto';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { Project } from '../../../core/models/project.model';
import { Client } from '../../../core/models/client.model';
import { EmployeeForProjectDto } from '../../../core/dtos/employee/employee-for-project.dto';
import { UpdateProjectDto } from '../../../core/dtos/project/update-project.dto';
import { AddOrRemoveEmployeeProjectDto } from '../../../core/dtos/employee/add-or-remove-employee-project.dto';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrl: './create-project.component.css'
})
export class CreateProjectComponent {

  private destroy$ = new Subject<void>();
  createProjectForm: FormGroup;
  isEditMode : boolean = false;
  buttonContent : string = 'Create Project';
  displayedColumns : string[] = ['index', 'name', 'surname' , 'remove']
  displayedColumnsForReport : string[] = ['index', 'name', 'surname' ]

  constructor(private fb: FormBuilder, private projectService : ProjectService,
              private clientService : ClientService, private employeeService : EmployeeService,
               private router: Router, private swal : AlertService) {
    this.createProjectForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      client:['', Validators.required],
      teamLead:[''],
    });
  }

  existingProjectId !: string
  clients : Client[] = []
  allDevelopers : Employee[] = []
  workingDevelopers : EmployeeForProjectDto[] = []
  availableDevelopers : EmployeeForProjectDto[] = []
  selectedDeveloperId !: string

  ngOnInit(){
    this.clientService.getAllClients("?page=1&items-per-page=100")
      .pipe(takeUntil(this.destroy$),tap( response => {
        this.clients = response.clients.map( (clientWithProjects: ClientWithNumberOfProjects) => clientWithProjects.client);
      })).subscribe()

      this.employeeService.getAllDevelopers()
      .pipe(takeUntil(this.destroy$),tap( response => {
        this.allDevelopers = response.developers;
      })).subscribe()

    this.existingProjectId = history.state.projectData
    if(this.existingProjectId){
      this.isEditMode = true;
      this.buttonContent = 'Update Project'
      this.projectService.getProjectById(this.existingProjectId).pipe(takeUntil(this.destroy$),
        tap( response => {
          this.workingDevelopers = response.working
          this.availableDevelopers = response.notWorking
          
          this.createProjectForm.patchValue({
            title: response.title,
            description: response.description,
            client: response.client.id,
            teamLead : response.teamLeadId
          });
        }),
        catchError( error => {
          this.swal.fireSwalError(error.error.detail)
          return throwError( () => error)
        })
      ).subscribe()  
    }
  }

  onSubmit() {
    if (this.createProjectForm.valid) {
      if(!this.isEditMode){
        const dto: CreateProjectDto = {
            title: this.createProjectForm.get('title')?.value,
            description: this.createProjectForm.get('description')?.value,
            clientId: this.createProjectForm.get('client')?.value,
            teamLeadId: this.createProjectForm.get('teamLead')?.value,
          };
        this.projectService.createProject(dto)
          .pipe(takeUntil(this.destroy$), tap((response) => {
            this.swal.fireSwalSuccess("Project created successfully")
            this.router.navigate(['projects'])
            }),
            catchError( error => {
              this.swal.fireSwalError("Something went wrong")
              return throwError(() => error);
            })).subscribe();
      }
      else
      {
        const dto: UpdateProjectDto = {
          id: this.existingProjectId,
          title: this.createProjectForm.get('title')?.value,
          description: this.createProjectForm.get('description')?.value,
          clientId: this.createProjectForm.get('client')?.value,
          teamLeadId: this.createProjectForm.get('teamLead')?.value,
        };
      this.projectService.updateProject(dto)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.swal.fireSwalSuccess("Project updated successfully")
          this.router.navigate(['projects'])
          }),
          catchError( error => {
            this.swal.fireSwalError("Something went wrong while updating project")
            return throwError(() => error);
          })).subscribe();
      }
    }
  }

  removeEmployeeFromProject(employeeId : string){
    const dto : AddOrRemoveEmployeeProjectDto = {
      employeeId : employeeId,
      projectId : this.existingProjectId
    }
    this.projectService.removeEmployeeFromProject(dto).pipe(takeUntil(this.destroy$),
      tap( (response) => {
        this.workingDevelopers = response.working;
        this.availableDevelopers = response.notWorking;
      }),
      catchError( error => {
        this.swal.fireSwalError("Something went wrong while removing employee")
        return throwError(() => error);
      })
    ).subscribe()
  }

  addEmployeeToProject(){
    const dto : AddOrRemoveEmployeeProjectDto = {
      employeeId : this.selectedDeveloperId,
      projectId : this.existingProjectId
    }
    this.projectService.addEmployeeToProject(dto).pipe(takeUntil(this.destroy$),
      tap( (response) => {
        this.workingDevelopers = response.working;
        this.availableDevelopers = response.notWorking;
      }),
      catchError( error => {
        this.swal.fireSwalError("Something went wrong while adding employee")
        return throwError(() => error);
      })
    ).subscribe()
  }

  generateReport() {
    const data = document.getElementById('pdf-content'); // The HTML element to capture
    if(data){
      const originalDisplay = data.style.display;
      data.style.display = 'block'; // or 'inline-block' depending on your needs

      html2canvas(data).then(canvas => {
        // Restore the original display style
        data.style.display = originalDisplay;

        const imgWidth = 208;
        const pageHeight = 295;
        const imgHeight = (canvas.height * imgWidth) / canvas.width;
        const position = 0;

        const imgData = canvas.toDataURL('image/png');
        const doc = new jsPDF();

        doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
        doc.save('captured-content.pdf');
      });
    }
  }

  getClientForReport(): string|undefined {
    let id = this.createProjectForm.get('client')?.value
    return this.clients.find(client => client.id === id)?.name;
  }

  getTeamLeadForReport(): string|undefined {
    let id = this.createProjectForm.get('teamLead')?.value
    let dev = this.workingDevelopers.find(dev => dev.id === id);
    return dev?.name + " " + dev?.surname
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
