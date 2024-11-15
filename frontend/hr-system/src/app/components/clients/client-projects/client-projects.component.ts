import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProjectService } from '../../../core/services/project.service';
import { Project } from '../../../core/models/project.model';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-client-projects',
  templateUrl: './client-projects.component.html',
  styleUrl: './client-projects.component.css'
})
export class ClientProjectsComponent {

  projects : Project[]= []

  constructor(@Inject(MAT_DIALOG_DATA) public data: { clientId: string }, private projectService : ProjectService,
              private readonly dialogRef: MatDialogRef<ClientProjectsComponent>, private swal : AlertService) {}

  private destroy$ = new Subject<void>()

  ngOnInit(){
    this.projectService.getProjectsByClientId(this.data.clientId).pipe(takeUntil(this.destroy$),
     tap( response => {
        this.projects = response.projects
     }),
    catchError( error => {
      this.swal.fireSwalError("Problem fetching projects for client")
      this.onCancel()
      return throwError(() => error)
    })).subscribe()
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
