import { Component, inject } from '@angular/core';
import { Subject, switchMap, takeUntil, tap } from 'rxjs';
import { Router } from '@angular/router';
import { HolidayRequest, HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { DatePipe } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SendHolidayRequestFormComponent } from '../send-holiday-request-form/send-holiday-request-form.component';
import { HolidayRequestApproverService } from '../../../core/services/holiday-request-approver.service';
import { HolidayRequestApprover } from '../../../core/models/holiday-request-approver.model.ts';

@Component({
  selector: 'app-holiday-requests-component',
  templateUrl: './holiday-requests.component.html',
  styleUrl: './holiday-requests.component.css',
  providers: [DatePipe],
})
export class HolidayRequestsComponent {
  readonly dialog = inject(MatDialog);
  // Sample Employees
 employees: Employee[] = [
  { id: '1', name: 'Alice', surname: 'Johnson', daysOff: 10, role: EmployeeRole.Developer, accountId: 'A001' },
  { id: '2', name: 'Bob', surname: 'Smith', daysOff: 8, role: EmployeeRole.Manager, accountId: 'A002' },
  { id: '3', name: 'Charlie', surname: 'Brown', daysOff: 5, role: EmployeeRole.Developer, accountId: 'A003' },
  { id: '4', name: 'Diana', surname: 'Miller', daysOff: 15, role: EmployeeRole.Manager, accountId: 'A004' },
  { id: '5', name: 'Eve', surname: 'Davis', daysOff: 12, role: EmployeeRole.Developer, accountId: 'A005' }
];

// Generate holiday requests
 holidayRequests: HolidayRequest[] = []
holidayRequestApprovers: HolidayRequestApprover[] = []

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 5;

  displayedColumns : string[] = ['status', 'sender', 'start', 'end', 'delete']
  approverColumns: string[] = ['sender', 'start', 'end', 'actions'];

  private destroy$ = new Subject<void>();

  constructor(private holidayRequestService: HolidayRequestService, private holidayRequestApproverService: HolidayRequestApproverService, private router: Router, private datePipe: DatePipe) {
    
  }

  ngOnInit() {
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequests = response.holidayRequests,
        this.totalPages = response.totalPages
      })).subscribe();

    this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId("0d2a2715-84f8-4b66-a710-6fcf2a62cbbb")
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequestApprovers = response.holidayRequestApprovers
      })).subscribe();
  }

  formatDate(date: Date): string {
    return this.datePipe.transform(date, 'yyyy-MMM-dd') ?? '';
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage;
  }

  loadNewPage(selectedPage : number) : void {
    this.pageNumber = selectedPage;
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
                        .pipe(takeUntil(this.destroy$), 
                        tap((response) =>{
           this.holidayRequests = response.holidayRequests
         this.totalPages = response.totalPages
          })).subscribe()
  }

  deleteHolidayRequest(id : string) : void {
    console.log("deleted")
    this.holidayRequestService.deleteHolidayRequest(id).pipe(takeUntil(this.destroy$),
     switchMap( () => this.holidayRequestService.getAllHolidayRequests(this.getQueryString()).pipe(
       tap(response => {
         this.holidayRequests = response.holidayRequests;
         this.totalPages = response.totalPages;
       })))).subscribe()
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getStatusIcon(status: HolidayRequestStatus): string {
    switch (status) {
      case HolidayRequestStatus.Approved:
        return 'check_circle';
      case HolidayRequestStatus.Pending:
        return 'pending';
      case HolidayRequestStatus.Denied:
        return 'cancel';
      default:
        return 'help_outline';
    }
  }
  getStatusString(status: HolidayRequestStatus): string {
    switch (status) {
      case HolidayRequestStatus.Approved:
        return 'Approved';
      case HolidayRequestStatus.Pending:
        return 'Pending';
      case HolidayRequestStatus.Denied:
        return 'Denied';
      default:
        return '-';
    }
  }
  getStatusColor(status: HolidayRequestStatus): string {
    switch (status) {
      case HolidayRequestStatus.Approved:
        return 'green';
      case HolidayRequestStatus.Pending:
        return '#eac60c';
      case HolidayRequestStatus.Denied:
        return '#c62828';
      default:
        return 'gray';
    }
  }

  getSenderName(requestId: string): string {
    const approver = this.employees.find(emp => emp.id === 'id');
    return approver ? `${approver.name} ${approver.surname}` : 'Unknown';
  }

  getHolidayRequestStartDate(requestId: string): Date{
    return new Date();
  }
  getHolidayRequestEndDate(requestId: string): Date{
    return new Date();
  }

  // Approve Request
approveRequest(approverId: string): void {
  const approver = this.holidayRequestApprovers.find(a => a.id === approverId);
  if (approver) {
    approver.status = HolidayRequestStatus.Approved;
    console.log(`Holiday request approved for ${approverId}`);
    // Call the service to update the approval status
    // this.holidayRequestService.updateApproverStatus(approverId, HolidayRequestStatus.Approved).subscribe();
  }
}

// Deny Request
denyRequest(approverId: string): void {
  const approver = this.holidayRequestApprovers.find(a => a.id === approverId);
  if (approver) {
    approver.status = HolidayRequestStatus.Denied;
    console.log(`Holiday request denied for ${approverId}`);
    // Call the service to update the denial status
    // this.holidayRequestService.updateApproverStatus(approverId, HolidayRequestStatus.Denied).subscribe();
  }
}

  openRequestDialog(): void{
    const dialogRef = this.dialog.open(SendHolidayRequestFormComponent, {
      height: '260px',
      width: '340px',
    });
  }
}