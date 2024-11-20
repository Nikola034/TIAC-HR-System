import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, debounceTime, Subject, switchMap, takeUntil, tap, throwError } from 'rxjs';
import { Project } from '../../../core/models/project.model';
import { ClientWithNumberOfProjects } from '../../../core/dtos/client/client-with-number-of-projects.dto';
import { ClientService } from '../../../core/services/client.service';
import Swal from 'sweetalert2'
import { AlertService } from '../../../core/services/alert.service';
import { MatDialog } from '@angular/material/dialog';
import { SendHolidayRequestFormComponent } from '../../holidayRequests/send-holiday-request-form/send-holiday-request-form.component';
import { ClientProjectsComponent } from '../client-projects/client-projects.component';
import { Client } from '../../../core/models/client.model';

@Component({
  selector: 'app-all-clients',
  templateUrl: './all-clients.component.html',
  styleUrl: './all-clients.component.css'
})
export class AllClientsComponent {
  clients !: ClientWithNumberOfProjects[]

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 8;
  displayedColumns : string[] = ['name', 'country' , 'numberOfProjects', 'edit', 'projects', 'delete']

  nameSearch : string = ''
  countrySearch : string = ''

  private searchParamChangeSubject = new Subject<void>();

  onPropertyChange() {
    this.searchParamChangeSubject.next(); // Emit the value to the Subject
  }

  private destroy$ = new Subject<void>();
  readonly dialog = inject(MatDialog);

  constructor(private clientService: ClientService, private router: Router, private swal : AlertService) {
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
    this.clientService.getAllClients(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.clients = response.clients
        this.totalPages = response.totalPages
      }),
        catchError( error => {
          this.swal.fireSwalError("Problem fetching clients")
          return throwError(() => error)
        })).subscribe();
    
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage + "&name=" + this.nameSearch.toLowerCase() + "&country=" + this.countrySearch.toLowerCase();
  }

  editClient(client: Client) : void {
    this.router.navigate(['edit-client'], { state: { clientData: client } })
  }

  viewProjects(clientId : string): void{
    const dialogRef = this.dialog.open(ClientProjectsComponent, {
      data: { clientId: clientId },
      width: '700px',
    });
  }

  loadNewPage(selectedPage : number) : void {
    this.pageNumber = selectedPage;
    this.clientService.getAllClients(this.getQueryString())
      .pipe(takeUntil(this.destroy$), 
      tap((response) =>{
          this.clients = response.clients
          this.totalPages = response.totalPages
      }),
      catchError( error => {
        this.swal.fireSwalError("Problem fetching clients")
        return throwError(() => error)
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
