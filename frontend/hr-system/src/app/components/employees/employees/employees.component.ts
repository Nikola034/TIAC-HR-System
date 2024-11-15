import { Component } from '@angular/core';
import { Employee } from '../../../core/models/employee.model';
import {
  catchError,
  combineLatest,
  forkJoin,
  map,
  Observable,
  of,
  Subject,
  switchMap,
  takeUntil,
  tap,
  throwError,
} from 'rxjs';
import { EmployeeService } from '../../../core/services/employee.service';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import Swal from 'sweetalert2';
import { AccountService } from '../../../core/services/account.service';
import { Account } from '../../../core/models/account.model';
import { SendAccountIdsDto } from '../../../core/dtos/account/send-account-ids.dto';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css',
})
export class EmployeesComponent {
  employees: Employee[] = [];
  accounts: Account[] = [];
  pageNumber: number = 1;
  totalPages: number = 1;
  itemsPerPage: number = 8;
  displayedColumns: string[] = [
    'name',
    'surname',
    'email',
    'daysOff',
    'role',
    'details',
    'delete',
  ];

  private destroy$ = new Subject<void>();

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private swal: AlertService,
    private accountservice: AccountService
  ) {}

  ngOnInit() {
    this.refreshData();
  }

  refreshData() {
    this.employeeService.getAllEmployees(this.getQueryString())
      .pipe(
        takeUntil(this.destroy$),
        switchMap((employeesResponse) => {
          const accountIds = this.getAccountIds(employeesResponse.employees);
          this.employees = employeesResponse.employees;
          this.totalPages = employeesResponse.totalPages
          return combineLatest([
            of(this.employees),
            this.accountservice.getAccountsByIds(accountIds)
          ]);
        }),
        tap(([employees, accountsResponse]) => {
          this.accounts = accountsResponse.accounts;
        }),
        catchError((error) => {
          this.swal.fireSwalError('Something went wrong');
          return throwError(() => error);
        })
      )
      .subscribe();
  }

  getQueryString(): string {
    return '?page=' + this.pageNumber + '&items-per-page=' + this.itemsPerPage;
  }

  getAccountIds(employees: Employee[]): SendAccountIdsDto {
    let ids: string[] = [];
    this.employees.forEach((element) => {
      ids.push(element.accountId);
    });
    const dto: SendAccountIdsDto = {
      ids: ids,
    };
    return dto;
  }

  editEmployee(employee: Employee): void {
    this.router.navigate(['edit-employee'], {
      state: { employeeData: employee },
    });
  }

  loadNewPage(selectedPage: number): void {
    this.pageNumber = selectedPage;
    this.refreshData();
  }

  delete(id: string): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete employee!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.employeeService
          .deleteEmployee(id)
          .pipe(
            takeUntil(this.destroy$),
            switchMap(() =>
              this.employeeService.getAllEmployees(this.getQueryString()).pipe(
                tap((response) => {
                  this.employees = response.employees;
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
        this.swal.fireSwalSuccess('Employee deleted successfully');
      }
    });
  }

  getEmailForEmployee(accountId: string): string | undefined {
    return this.accounts.find((account) => account.id == accountId)?.email;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
