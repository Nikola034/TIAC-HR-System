import { Component } from '@angular/core';
import { GetAllClientsDto } from '../../../core/dtos/client/get-all-clients.dto';
import { Router } from '@angular/router';
import { Subject, takeUntil, tap } from 'rxjs';
import { Project } from '../../../core/models/project.model';
import { ProjectService } from '../../../core/services/project.service';
import { ClientWithNumberOfProjects } from '../../../core/dtos/client/client-with-number-of-projects.dto';
import { ClientService } from '../../../core/services/client.service';

@Component({
  selector: 'app-all-clients',
  templateUrl: './all-clients.component.html',
  styleUrl: './all-clients.component.css'
})
export class AllClientsComponent {
  clients : ClientWithNumberOfProjects[] = [ 
    {client:{id:"id",name:"Clients name",country:"cnt"},numberOfProjects:2},
    {client:{id:"id",name:"Clients name",country:"cnt"},numberOfProjects:2},
    {client:{id:"id",name:"Clients name",country:"cnt"},numberOfProjects:2}]

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 8;
  displayedColumns : string[] = ['name', 'country' , 'numberOfProjects', 'projects', 'delete']

  private destroy$ = new Subject<void>();

  constructor(private clientService: ClientService, private router: Router) {}

  ngOnInit() {
    this.clientService.getAllClients(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.clients = response.clients
        this.totalPages = response.totalPages
      })).subscribe();
    
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage;
  }

  viewProjects(project:Project) : void {
    //this.router.navigate(['project/' + project.id])
  }

  loadNewPage(selectedPage : number) : void {
    this.pageNumber = selectedPage;
    this.clientService.getAllClients(this.getQueryString())
                        .pipe(takeUntil(this.destroy$), 
                        tap((response) =>{
           this.clients = response.clients
         this.totalPages = response.totalPages
          })).subscribe()
  }

  delete(id : string) : void {
    console.log("deleted")
    //this.projectService.delete(id).subscribe
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
