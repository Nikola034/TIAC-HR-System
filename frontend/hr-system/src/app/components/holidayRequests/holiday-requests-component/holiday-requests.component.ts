import { Component } from '@angular/core';
import { Subject, takeUntil, tap } from 'rxjs';
import { Router } from '@angular/router';
import { HolidayRequest, HolidayRequestStatus } from '../../../core/models/holiday-request.model';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-holiday-requests-component',
  templateUrl: './holiday-requests.component.html',
  styleUrl: './holiday-requests.component.css',
  providers: [DatePipe],
})
export class HolidayRequestsComponent {
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
  { id: 'HR8', start: new Date('2024-08-12'), end: new Date('2024-08-20'), sender: this.employees[2], status: HolidayRequestStatus.Pending },
  { id: 'HR9', start: new Date('2024-09-10'), end: new Date('2024-09-15'), sender: this.employees[3], status: HolidayRequestStatus.Approved },
  { id: 'HR10', start: new Date('2024-10-05'), end: new Date('2024-10-12'), sender: this.employees[4], status: HolidayRequestStatus.Denied }
];

  pageNumber : number = 1;
  totalPages : number = 1;
  itemsPerPage : number = 10;
  displayedColumns : string[] = ['status', 'sender', 'start', 'end', 'delete']

  private destroy$ = new Subject<void>();

  constructor(private holidayRequestService: HolidayRequestService, private router: Router, private datePipe: DatePipe) {}

  ngOnInit() {
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequests = response.holidayRequests
      })).subscribe();
    
  }

  formatDate(date: Date): string {
    return this.datePipe.transform(date, 'yyyy-MMM-dd') ?? '';
  }

  getQueryString() : string {
    return '?page=' + this.pageNumber + "&items-per-page=" + this.itemsPerPage;
  }

  viewProject(HolidayRequest: HolidayRequest) : void {
    this.router.navigate(['holidayRequest/' + HolidayRequest.id])
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
    this.holidayRequestService.getAllHolidayRequests(this.getQueryString())
                        .pipe(takeUntil(this.destroy$), 
                        tap((response) =>{
           this.holidayRequests = response.holidayRequests
         this.totalPages = response.totalPages
          })).subscribe()
  }

  delete(id : string) : void {
    console.log("deleted")
    //this.holidayRequestService.delete(id).subscribe
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
        return 'hourglass_empty';
      case HolidayRequestStatus.Denied:
        return 'cancel';
      default:
        return 'help_outline';
    }
  }

  getStatusColor(status: HolidayRequestStatus): string {
    switch (status) {
      case HolidayRequestStatus.Approved:
        return 'green';
      case HolidayRequestStatus.Pending:
        return 'gray';
      case HolidayRequestStatus.Denied:
        return 'red';
      default:
        return 'gray';
    }
  }
}
