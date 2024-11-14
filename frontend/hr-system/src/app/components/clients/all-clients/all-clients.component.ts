import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, switchMap, takeUntil, tap } from 'rxjs';
import { Project } from '../../../core/models/project.model';
import { ClientWithNumberOfProjects } from '../../../core/dtos/client/client-with-number-of-projects.dto';
import { ClientService } from '../../../core/services/client.service';
import Swal from 'sweetalert2'
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-all-clients',
  templateUrl: './all-clients.component.html',
  styleUrl: './all-clients.component.css'
})
export class AllClientsComponent {
  clients : ClientWithNumberOfProjects[] = []

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 8;
  displayedColumns : string[] = ['name', 'country' , 'numberOfProjects', 'projects', 'delete']

  private destroy$ = new Subject<void>();

  constructor(private clientService: ClientService, private router: Router, private swal : AlertService) {}

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
    Swal.fire({
      title: "Are you sure?",
      text: "All projects for this client will be deleted!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        this.clientService.deleteClient(id).pipe(takeUntil(this.destroy$),
      switchMap( () => this.clientService.getAllClients(this.getQueryString()).pipe(
       tap(response => {
         this.clients = response.clients;
         this.totalPages = response.totalPages;
       })))).subscribe()
        this.swal.fireSwalSuccess("Client deleted successfully")
      }
    });
    
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
