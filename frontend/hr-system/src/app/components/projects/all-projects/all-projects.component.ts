import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from '../../../core/models/project.model';
import { ProjectService } from '../../../core/services/project.service';
import { Router } from '@angular/router';
import { debounceTime, Subject, switchMap, takeUntil, tap } from 'rxjs';
import Swal from 'sweetalert2'
import { AlertService } from '../../../core/services/alert.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html',
  styleUrl: './all-projects.component.css'
})
export class AllProjectsComponent implements OnInit, OnDestroy {
  projects !: Project[];
  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 8;
  displayedColumns : string[] = ['title', 'description' , 'client', 'details', 'delete']

  titleSearch : string = ''
  descriptionSearch : string = ''
  clientSearch : string = ''

  private searchParamChangeSubject = new Subject<void>();

  onPropertyChange() {
    this.searchParamChangeSubject.next(); // Emit the value to the Subject
  }

  private destroy$ = new Subject<void>();

  constructor(private projectService: ProjectService, private router: Router, private swal :AlertService) {
    this.searchParamChangeSubject
      .pipe(
        debounceTime(500), // Wait for 500ms pause in events
        switchMap(async () => this.loadNewPage(1)) // Cancel previous requests and start new one
      )
      .subscribe((response) => {
        console.log('Response:', response);
      });
  }

  ngOnInit() {
    this.projectService.getAllProjects(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.projects = response.projects
        this.totalPages = response.totalPages
      })).subscribe();
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage
    + "&title=" + this.titleSearch.toLowerCase() + "&description=" + this.descriptionSearch.toLowerCase() + "&client=" + this.clientSearch.toLowerCase();
  }



  editProject(projectId: string) : void {
    this.router.navigate(['edit-project'], { state: { projectData: projectId } })
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
