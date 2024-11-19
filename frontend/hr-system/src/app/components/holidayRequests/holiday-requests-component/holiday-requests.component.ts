import { Component, inject, OnInit, ViewChild } from '@angular/core';
import {
  catchError,
  map,
  of,
  Subject,
  switchMap,
  take,
  takeUntil,
  tap,
  throwError,
} from 'rxjs';
import { Router } from '@angular/router';
import {
  HolidayRequest,
  HolidayRequestStatus,
} from '../../../core/models/holiday-request.model';
import { HolidayRequestService } from '../../../core/services/holiday-request.service';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { DatePipe } from '@angular/common';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SendHolidayRequestFormComponent } from '../send-holiday-request-form/send-holiday-request-form.component';
import { HolidayRequestApproverService } from '../../../core/services/holiday-request-approver.service';
import { HolidayRequestApprover } from '../../../core/models/holiday-request-approver.model.ts';
import { UpdateHolidayRequestApproverDto } from '../../../core/dtos/holiday-request-approver/update-holiday-request-approver.dto';
import { JwtService } from '../../../core/services/jwt.service';
import Swal from 'sweetalert2';
import { AlertService } from '../../../core/services/alert.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { GetSenderForApproverDto } from '../../../core/dtos/holiday-request/get-sender-for-approver.dto';
import { GetAllHolidayRequestApproversByApproverIdDto } from '../../../core/dtos/holiday-request/get-all-holiday-request-approves-by-approverid.dto';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { DaysOffReportDto } from '../../../core/dtos/employee/days-off-report.dto';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-holiday-requests-component',
  templateUrl: './holiday-requests.component.html',
  styleUrl: './holiday-requests.component.css',
  providers: [DatePipe],
})
export class HolidayRequestsComponent{
  readonly dialog = inject(MatDialog);

  holidayRequests: HolidayRequest[] = [];
  filteredHolidayRequests: HolidayRequest[] = [];
  holidayRequestApprovers: GetAllHolidayRequestApproversByApproverIdDto[] = [];
  
  dataSource = new MatTableDataSource<any>();
  report: DaysOffReportDto | undefined

  pageNumber: number = 1;
  totalPages: number = 1;
  itemsPerPage: number = 5;

  displayedColumns: string[] = ['status', 'sender', 'start', 'end', 'delete'];
  approverColumns: string[] = ['sender', 'start', 'end', 'actions'];
  reportColumns: string[] = ['used', 'remaining', 'pending'];

  private destroy$ = new Subject<void>();

  constructor(
    private employeeService: EmployeeService,
    private holidayRequestService: HolidayRequestService,
    public jwtService: JwtService,
    private swal: AlertService,
    private holidayRequestApproverService: HolidayRequestApproverService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.getDaysOffReport();
    this.refreshHolidayRequests();
    this.refreshHolidayRequestApprovers();
  }

  getDaysOffReport(): void{
    this.employeeService.getDaysOffForEmployee(this.jwtService.getIdFromToken())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.dataSource.data = [response]
        this.report = response
      })).subscribe();
  }

  refreshHolidayRequests(): void {
    this.holidayRequestService.getAllHolidayRequestsBySenderId(this.jwtService.getIdFromToken(), this.getQueryString())
      .pipe(takeUntil(this.destroy$), tap((response) =>{
        this.holidayRequests = response.holidayRequests
        this.totalPages = response.totalPages
      })).subscribe();
  }

  refreshHolidayRequestApprovers(): void{
    this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(this.jwtService.getIdFromToken()).
    pipe(
      takeUntil(this.destroy$),
      tap((response) => {
        this.holidayRequestApprovers = response.holidayRequestApprovers;
      })
    ).subscribe();
  }

  getSenderForApprover(id: string) : string{
    return this.holidayRequests.find(x => x.id == id)?.sender.name + ' ' + this.holidayRequests.find(x => x.id == id)?.sender.surname || 'Loading'
  }

  formatDate(date: Date | undefined): string {
    return this.datePipe.transform(date, 'yyyy-MMM-dd') ?? '';
  }

  getQueryString(): string {
    return '?page=' + this.pageNumber + '&items-per-page=' + this.itemsPerPage;
  }

  loadNewPage(selectedPage: number): void {
    this.pageNumber = selectedPage;
    this.refreshHolidayRequests();
  }

  deleteHolidayRequest(id: string): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.holidayRequestService
          .deleteHolidayRequest(id)
          .pipe(
            takeUntil(this.destroy$),
            switchMap(() =>
              this.holidayRequestService
                .getAllHolidayRequestsBySenderId(this.jwtService.getIdFromToken(), this.getQueryString())
                .pipe(
                  tap((response) => {
                    this.holidayRequests = response.holidayRequests;
                    if(this.pageNumber > response.totalPages){
                      this.totalPages = response.totalPages
                      this.pageNumber = response.totalPages;
                      this.loadNewPage(this.totalPages)
                    }
                    this.totalPages = response.totalPages;
                    this.getDaysOffReport();
                  }),
                  catchError((error) => {
                    this.swal.fireSwalError('Something went wrong');
                    return throwError(() => error);
                  })
                )
            )
          )
          .subscribe();
        this.swal.fireSwalSuccess('Holday request deleted successfully');
      }
    });
  }

  approveHolidayRequestApprover(requestId: string, approverId: string): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Request will be approved!',
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, approve it!',
    }).then((result) => {
      if (result.isConfirmed) {
        const dto: UpdateHolidayRequestApproverDto = {
          requestId: requestId,
          approverId: approverId,
          status: HolidayRequestStatus.Approved,
        };

        this.holidayRequestApproverService
          .updateHolidayRequestApprover(dto)
          .pipe(
            takeUntil(this.destroy$),
            switchMap(() =>
              this.holidayRequestApproverService
                .getAllHolidayRequestApproversByApproverId(approverId)
                .pipe(
                  tap((response) => {
                    this.holidayRequestApprovers =
                      response.holidayRequestApprovers;
                      this.refreshHolidayRequestApprovers();
                  }),
                  catchError((error) => {
                    this.swal.fireSwalError('Something went wrong');
                    return throwError(() => error);
                  })
                )
            )
          )
          .subscribe();
        this.swal.fireSwalSuccess('Holiday request approved successfully');
      }
    });
  }

  denyHolidayRequestApprover(requestId: string, approverId: string): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "Request will be denied! You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, deny it!',
    }).then((result) => {
      if (result.isConfirmed) {
        const dto: UpdateHolidayRequestApproverDto = {
          requestId: requestId,
          approverId: approverId,
          status: HolidayRequestStatus.Denied,
        };
        this.holidayRequestApproverService
          .updateHolidayRequestApprover(dto)
          .pipe(
            takeUntil(this.destroy$),
            switchMap(() =>
              this.holidayRequestApproverService
                .getAllHolidayRequestApproversByApproverId(approverId)
                .pipe(
                  tap((response) => {
                    this.holidayRequestApprovers =
                      response.holidayRequestApprovers;
                      this.refreshHolidayRequestApprovers();
                  }),
                  catchError((error) => {
                    this.swal.fireSwalError('Something went wrong');
                    return throwError(() => error);
                  })
                )
            )
          )
          .subscribe();
        this.swal.fireSwalSuccess('Holiday request denied successfully');
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

  getHolidayRequestStartDate(requestId: string): Date | undefined {
    return this.holidayRequests.find((x) => x.id == requestId)?.start;
  }

  getHolidayRequestEndDate(requestId: string): Date | undefined {
    return this.holidayRequests.find((x) => x.id == requestId)?.end;
  }

  generateReport(){
    const data = document.getElementById('pdf-content'); // The HTML element to capture
    if(data){
      const originalDisplay = data.style.display;
      data.style.display = 'block'; // or 'inline-block' depending on your needs

      html2canvas(data).then(canvas => {
        // Restore the original display style
        data.style.display = originalDisplay;

        const imgWidth = 208;
        const pageHeight = 295;
        const imgHeight = (canvas.height * imgWidth) / canvas.width;
        const position = 0;

        const imgData = canvas.toDataURL('image/png');
        const doc = new jsPDF();

        doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
        doc.save(this.dataSource.data[0].report.employee.name + ' ' + this.dataSource.data[0].report.employee.surname + ' DaysOff Report ' + this.formatDate(new Date));
      });
    }
  }

  openRequestDialog(): void {
    
    const dialogRef = this.dialog.open(SendHolidayRequestFormComponent, {
      height: '260px',
      width: '340px',
    });

    dialogRef.afterClosed().subscribe((newRequest: HolidayRequest) => {
      this.holidayRequests.push(newRequest);
      this.refreshHolidayRequests();
      this.refreshHolidayRequestApprovers();
      this.getDaysOffReport();
    });
  }
}
