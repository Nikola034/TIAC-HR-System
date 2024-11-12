import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table'; 
import { ProjectService } from '../../../services/project.service';
import { Router } from '@angular/router';
import { Project } from '../../../model/entities/Project';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-all-projects',
  standalone: true,
  imports: [
    MatTableModule,
    CommonModule
  ],
  templateUrl: './all-projects.component.html',
  styleUrl: './all-projects.component.css'
})
export class AllProjectsComponent {
    projects : Project[] = [];
    pageNumber : number = 1;
    totalPages : number = 1;
    itemsPerPage : number = 10;
    displayedColumns : string[] = ['title', 'description' , 'client']

    constructor(private projectService: ProjectService, private router: Router) {}

    ngOnInit() {
      this.projectService.getAllProjects(this.getQueryString()).subscribe( response => {
          this.projects = response.projects
          this.totalPages = response.totalPages
        }
      )
    }

    getQueryString() : string {
      return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage;
    }

    viewProject(project:Project) : void {
      this.router.navigate(['project/' + project.id])
    }

    generatePagination(currentPage: number, totalPages: number): number[] {
      const maxVisiblePages = 5; // Number of page links to display at a time
      const paginationNumbers: number[] = [];
    
      let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
      let endPage = startPage + maxVisiblePages - 1;
    
      if (endPage > totalPages) {
        endPage = totalPages;
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
      }
    
      for (let i = startPage; i <= endPage; i++) {
        paginationNumbers.push(i);
      }
    
      return paginationNumbers;
    }

    loadNewPage(selectedPage : number) : void {
      this.pageNumber = selectedPage;
      this.projectService.getAllProjects(this.getQueryString()).subscribe( response=> {
        this.projects = response.projects
        this.totalPages = response.totalPages
      })
    }
}
