import { Component, inject } from '@angular/core';
import { catchError, Subject, switchMap, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { HolidayRequest, HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { DatePipe } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SendHolidayRequestFormComponent } from '../send-holiday-request-form/send-holiday-request-form.component';
import { HolidayRequestApproverService } from '../../../core/services/holiday-request-approver.service';
import { HolidayRequestApprover } from '../../../core/models/holiday-request-approver.model.ts';
import { UpdateHolidayRequestApproverDto } from '../../../core/dtos/holiday-request-approver/update-holiday-request-approver.dto'
import { JwtService } from '../../../core/services/jwt.service';
import Swal from 'sweetalert2';
import { AlertService } from '../../../core/services/alert.service';

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

  constructor(private holidayRequestService: HolidayRequestService, private jwtService: JwtService, private swal: AlertService, private holidayRequestApproverService: HolidayRequestApproverService, private router: Router, private datePipe: DatePipe) {
    
  }

  ngOnInit() {
    this.refreshHolidayRequests()
    this.refreshHolidayRequestApprovers()
  }

  refreshHolidayRequests(): void{
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequests = response.holidayRequests,
        this.totalPages = response.totalPages
      })).subscribe();
  }

  refreshHolidayRequestApprovers(): void{
    this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(this.jwtService.getIdFromToken())
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
        this.holidayRequestService.deleteHolidayRequest(id).pipe(takeUntil(this.destroy$),
      switchMap( () => this.holidayRequestService.getAllHolidayRequests(this.getQueryString()).pipe(
       tap(response => {
         this.holidayRequests = response.holidayRequests;
         this.totalPages = response.totalPages;
       }),
       catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
        })))).subscribe()
        this.swal.fireSwalSuccess("Holday request deleted successfully")
      }
    });
  }

  approveHolidayRequestApprover(requestId: string, approverId: string) : void{
    Swal.fire({
      title: "Are you sure?",
      text: "Request will be approved!",
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, approve it!"
    }).then((result) => {
      if (result.isConfirmed) {
        const dto: UpdateHolidayRequestApproverDto = {
          requestId: requestId,
          approverId: approverId,
          status: HolidayRequestStatus.Approved
        }

        this.holidayRequestApproverService.updateHolidayRequestApprover(dto).pipe(takeUntil(this.destroy$),
      switchMap( () => this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(approverId).pipe(
       tap(response => {
         this.holidayRequestApprovers = response.holidayRequestApprovers;
       }),
       catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
        })))).subscribe()
        this.swal.fireSwalSuccess("Holiday request approved successfully")
      }
    });
  }

  denyHolidayRequestApprover(requestId: string, approverId: string) : void{
    Swal.fire({
      title: "Are you sure?",
      text: "Request will be denied! You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, deny it!"
    }).then((result) => {
      if (result.isConfirmed) {
        const dto: UpdateHolidayRequestApproverDto = {
          requestId: requestId,
          approverId: approverId,
          status: HolidayRequestStatus.Denied
        }

        this.holidayRequestApproverService.updateHolidayRequestApprover(dto).pipe(takeUntil(this.destroy$),
      switchMap( () => this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(approverId).pipe(
       tap(response => {
         this.holidayRequestApprovers = response.holidayRequestApprovers;
       }),
       catchError( error => {
        this.swal.fireSwalError("Something went wrong")
        return throwError(() => error);
        })
      ))).subscribe()
        this.swal.fireSwalSuccess("Holiday request denied successfully")
      }
    });
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

  openRequestDialog(): void{
    const dialogRef = this.dialog.open(SendHolidayRequestFormComponent, {
      height: '260px',
      width: '340px',
    });

    dialogRef.afterClosed().subscribe(() => {
      this.refreshHolidayRequests();
      this.refreshHolidayRequestApprovers();
    })
  }
}