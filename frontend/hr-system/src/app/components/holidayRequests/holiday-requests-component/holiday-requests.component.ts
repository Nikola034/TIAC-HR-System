import { Component, inject } from '@angular/core';
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

@Component({
  selector: 'app-holiday-requests-component',
  templateUrl: './holiday-requests.component.html',
  styleUrl: './holiday-requests.component.css',
  providers: [DatePipe],
})
export class HolidayRequestsComponent {
  readonly dialog = inject(MatDialog);

  holidayRequests: HolidayRequest[] = [];
  filteredHolidayRequests: HolidayRequest[] = [];
  holidayRequestApprovers: GetAllHolidayRequestApproversByApproverIdDto[] = [];

  pageNumber: number = 1;
  totalPages: number = 1;
  itemsPerPage: number = 5;

  displayedColumns: string[] = ['status', 'sender', 'start', 'end', 'delete'];
  approverColumns: string[] = ['sender', 'start', 'end', 'actions'];

  private destroy$ = new Subject<void>();

  constructor(
    private employeeService: EmployeeService,
    private holidayRequestService: HolidayRequestService,
    private jwtService: JwtService,
    private swal: AlertService,
    private holidayRequestApproverService: HolidayRequestApproverService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.refreshHolidayRequests();
  }

  refreshHolidayRequests(): void {
    if (this.jwtService.getRoleFromToken() == 'Manager') {
      this.holidayRequestService.getAllHolidayRequestsBySenderId(this.jwtService.getIdFromToken(), this.getQueryString())
    .pipe(
      takeUntil(this.destroy$),
      switchMap((holidayResponse) => {
        this.holidayRequests = holidayResponse.holidayRequests;
        this.totalPages = holidayResponse.totalPages;

        return this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(this.jwtService.getIdFromToken()).pipe(
          map((approversResponse) => ({
            holidays: holidayResponse.holidayRequests,
            approvers: approversResponse
          }))
        );
      }),
      tap(({ holidays, approvers }) => {
        this.holidayRequests = holidays;
        this.holidayRequestApprovers = approvers;  
      }),
      catchError((error) => {
        this.swal.fireSwalError('Something went wrong');
        return throwError(() => error);
      })
    )
    .subscribe();
    } else {
      this.holidayRequestService.getAllHolidayRequestsBySenderId(this.jwtService.getIdFromToken(), this.getQueryString())
    .pipe(
      takeUntil(this.destroy$),
      switchMap((holidayResponse) => {
        this.holidayRequests = holidayResponse.holidayRequests;
        this.totalPages = holidayResponse.totalPages;
        return this.holidayRequestApproverService.getAllHolidayRequestApproversByApproverId(this.jwtService.getIdFromToken()).pipe(
          map((approversResponse) => ({
            holidays: holidayResponse.holidayRequests,
            approvers: approversResponse,
          }))
        );
      }),
      tap(({ holidays, approvers }) => {
        this.holidayRequests = holidays;
        this.holidayRequestApprovers = approvers;  
      }),
      catchError((error) => {
        this.swal.fireSwalError('Something went wrong');
        return throwError(() => error);
      })
    )
    .subscribe();
    }
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
    this.refreshHolidayRequests()
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
                .getAllHolidayRequests(this.getQueryString())
                .pipe(
                  tap((response) => {
                    this.holidayRequests = response.holidayRequests;
                    this.totalPages = response.totalPages;
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
                      response;
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
                      response;
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

  openRequestDialog(): void {
    const dialogRef = this.dialog.open(SendHolidayRequestFormComponent, {
      height: '260px',
      width: '340px',
    });

    dialogRef.afterClosed().subscribe((newRequest: HolidayRequest) => {
      this.holidayRequests.push(newRequest);
      this.refreshHolidayRequests();
    });
  }
}
