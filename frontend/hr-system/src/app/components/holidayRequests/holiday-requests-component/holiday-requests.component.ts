import { Component, inject } from '@angular/core';
import { Subject, takeUntil, tap } from 'rxjs';
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
 holidayRequests: HolidayRequest[] = [
  { id: 'HR1', start: new Date('2024-01-01'), end: new Date('2024-01-07'), sender: this.employees[0], status: HolidayRequestStatus.Approved },
  { id: 'HR2', start: new Date('2024-02-10'), end: new Date('2024-02-20'), sender: this.employees[1], status: HolidayRequestStatus.Pending },
  { id: 'HR3', start: new Date('2024-03-15'), end: new Date('2024-03-22'), sender: this.employees[2], status: HolidayRequestStatus.Denied },
  { id: 'HR4', start: new Date('2024-04-05'), end: new Date('2024-04-10'), sender: this.employees[3], status: HolidayRequestStatus.Approved },
  { id: 'HR5', start: new Date('2024-05-20'), end: new Date('2024-05-30'), sender: this.employees[4], status: HolidayRequestStatus.Pending },
  { id: 'HR6', start: new Date('2024-06-10'), end: new Date('2024-06-17'), sender: this.employees[0], status: HolidayRequestStatus.Approved },
  { id: 'HR7', start: new Date('2024-07-01'), end: new Date('2024-07-07'), sender: this.employees[1], status: HolidayRequestStatus.Denied },
];
holidayRequestApprovers: HolidayRequestApprover[] = [
  { id: 'A1', requestId: 'HR1', approverId: '2', status: HolidayRequestStatus.Approved },
  { id: 'A2', requestId: 'HR2', approverId: '3', status: HolidayRequestStatus.Pending },
  { id: 'A3', requestId: 'HR3', approverId: '4', status: HolidayRequestStatus.Denied },
  // Add more approvers as needed
];

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 10;

  displayedColumns : string[] = ['status', 'sender', 'start', 'end', 'delete']
  approverColumns: string[] = ['sender', 'start', 'end', 'actions'];

  private destroy$ = new Subject<void>();

  constructor(private holidayRequestService: HolidayRequestService, private holidayRequestApproverService: HolidayRequestApproverService, private router: Router, private datePipe: DatePipe) {
    
  }

  ngOnInit() {
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequests = response.holidayRequests
      })).subscribe();

    this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(this.getQueryString())
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
    //this.holidayRequestService.delete(id).subscribe
  }

  deleteHolidayRequestApprover(id : string) : void {
    console.log("deleted")
    //this
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