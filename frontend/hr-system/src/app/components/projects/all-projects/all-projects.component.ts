import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from '../../../core/models/project.model';
import { ProjectService } from '../../../core/services/project.service';
import { Router } from '@angular/router';
import { Subject, switchMap, takeUntil, tap } from 'rxjs';
import Swal from 'sweetalert2'
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html',
  styleUrl: './all-projects.component.css'
})
export class AllProjectsComponent implements OnInit, OnDestroy {
  projects : Project[] = [  ];
  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 8;
  displayedColumns : string[] = ['title', 'description' , 'client', 'details', 'delete']

  private destroy$ = new Subject<void>();

  constructor(private projectService: ProjectService, private router: Router, private swal :AlertService) {}

  ngOnInit() {
    this.projectService.getAllProjects(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.projects = response.projects
        this.totalPages = response.totalPages
      })).subscribe();
    
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage;
  }

  viewProject(project:Project) : void {
    this.router.navigate(['project/' + project.id])
  }

  loadNewPage(selectedPage : number) : void {
    this.pageNumber = selectedPage;
    this.projectService.getAllProjects(this.getQueryString())
                        .pipe(takeUntil(this.destroy$), 
                        tap((response) =>{
           this.projects = response.projects
         this.totalPages = response.totalPages
          })).subscribe()
  }

  delete(id : string) : void {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        this.projectService.deleteProject(id).pipe(takeUntil(this.destroy$),
          switchMap( () => this.projectService.getAllProjects(this.getQueryString()).pipe(
          tap(response => {
            this.projects = response.projects;
            this.totalPages = response.totalPages;
          })))).subscribe()
        this.swal.fireSwalSuccess("Project deleted successfully")
      }
    });
    
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
