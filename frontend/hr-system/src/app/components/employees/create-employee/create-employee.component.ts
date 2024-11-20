import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, Subject, switchMap, takeUntil, tap, throwError } from 'rxjs';
import { Employee } from '../../../core/models/employee.model';
import { CreateEmployeeDto } from '../../../core/dtos/employee/create-employee.dto';
import { EmployeeService } from '../../../core/services/employee.service';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { AccountService } from '../../../core/services/account.service';
import { CreateAccountDto } from '../../../core/dtos/employee/create-account.dto';
import { Account } from '../../../core/models/account.model';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrl: './create-employee.component.css'
})
export class CreateEmployeeComponent {
  createEmployeeForm: FormGroup;

  private destroy$ = new Subject<void>();
  isEditMode : boolean = false;
  buttonContent : string = 'Create Project';
  existingEmployee !: Employee
  account!: Account;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private router: Router, private swal : AlertService, private accountService: AccountService) {
    this.createEmployeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      daysOff: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (this.createEmployeeForm.valid) {
      if(!this.isEditMode){
        const accountDto: CreateAccountDto = {
          email: this.createEmployeeForm.get('email')?.value,
          password: this.generateRandomString(20),
        };
        const employeeDto: CreateEmployeeDto = {
            name: this.createEmployeeForm.get('name')?.value,
            surname: this.createEmployeeForm.get('surname')?.value,
            role: Number(this.createEmployeeForm.get('role')?.value),
            daysOff: this.createEmployeeForm.get('daysOff')?.value,
            accountId: ""
          };
        this.accountService.createAccount(accountDto).pipe(
          switchMap((accountResponse) => {
            employeeDto.accountId = accountResponse.id
            return this.employeeService.createEmployee(employeeDto);
          }),
          tap((employeeResponse) => {
            this.swal.fireSwalSuccess('Success', 'Employee created successfully!');
            this.router.navigate(['employees'])
          }),
          catchError((error) => {
            this.swal.fireSwalError('Something went wrong');
             return of(null); 
          })
        ).subscribe()
      }
      else
      {
        
      }
    }
  }

  generateRandomString(length: number): string {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let result = '';
    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * characters.length);
      result += characters[randomIndex];
    }
    return result;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
