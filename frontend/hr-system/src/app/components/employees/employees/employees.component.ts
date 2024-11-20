import { Component } from '@angular/core';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import {
  catchError,
  combineLatest,
  concatMap,
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
  itemsPerPage: number = 10;
  displayedColumns: string[] = [
    'name',
    'surname',
    'email',
    'daysOff',
    'role',
    'details',
    'delete',
  ];

  search = ''
  searchCriteria = 'name'
  roleFilter = 'all'

  private destroy$ = new Subject<void>();

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private swal: AlertService,
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.refreshData();
  }

  refreshData() {
    this.employeeService.getAllEmployees(this.getQueryString())
    .pipe(
      takeUntil(this.destroy$),
      switchMap((employeesResponse) => {
        this.employees = employeesResponse.employees;
        if(this.pageNumber > employeesResponse.totalPages){
          this.totalPages = employeesResponse.totalPages
          this.pageNumber = employeesResponse.totalPages;
          this.loadNewPage(this.totalPages)
        }
        this.totalPages = employeesResponse.totalPages;
        const accountIds = this.getAccountIds(employeesResponse.employees);

        return this.accountService.getAccountsByIds(accountIds).pipe(
          map((accountsResponse) => ({
            employees: employeesResponse.employees,
            accounts: accountsResponse.accounts,
          }))
        );
      }),
      tap(({ employees, accounts }) => {
        this.employees = employees;
        this.accounts = accounts;  
      }),
      catchError((error) => {
        this.swal.fireSwalError('Something went wrong');
        return throwError(() => error);
      })
    )
    .subscribe();
  }

  getRoleString(role: EmployeeRole) : string{
    switch(role) { 
      case 0: { 
        return 'Developer'
      } 
      case  1: { 
        return 'Manager'
      } 
      default: { 
         return ''
      } 
   } 
  }

  getQueryString(): string {
    if(this.search == ''){
      return '?page=' + this.pageNumber + '&items-per-page=' + this.itemsPerPage + '&role=' + this.roleFilter;
    }
    return '?page=' + this.pageNumber + '&items-per-page=' + this.itemsPerPage + `&${this.searchCriteria}=` + this.search.toLowerCase() + '&role=' + this.roleFilter;
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

  editEmployee(employeeId: string): void {
    this.router.navigate(['edit-employee'], {
      state: { employeeId: employeeId},
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
                  if(this.pageNumber > response.totalPages){
                    this.totalPages = response.totalPages
                    this.pageNumber = response.totalPages;
                    this.loadNewPage(this.totalPages)
                  }
                  this.totalPages = response.totalPages;
                  this.refreshData()
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
    return this.accounts.find(account => account.id === accountId)?.email || 'Loading...'
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
