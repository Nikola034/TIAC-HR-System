import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../../../core/services/employee.service';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { catchError, Subject, takeUntil, tap, throwError } from 'rxjs';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';
import { UpdateEmployeeDto } from '../../../core/dtos/employee/update-employee.dto';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrl: './edit-employee.component.css',
})
export class EditEmployeeComponent {
  editEmployeeForm: FormGroup;
  private destroy$ = new Subject<void>();
  employee: Employee | any
  selectedRole: EmployeeRole = EmployeeRole.Developer

  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private router: Router, private swal : AlertService) {
    this.editEmployeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });
  }

  ngOnInit(){
    this.employeeService.getEmployeeById(history.state.employeeId)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.employee = response
          this.editEmployeeForm.patchValue({
            name: this.employee.name,
            surname: this.employee.surname,
            role: this.employee.role
          });
          }),
          catchError( error => {
            this.swal.fireSwalError("Something went wrong while getting employee")
            return throwError(() => error);
          })).subscribe();
  }

  onSubmit() {
    if (this.editEmployeeForm.valid) {
      const dto: UpdateEmployeeDto = {
        id: this.employee.id,
        name: this.editEmployeeForm.get('name')?.value,
        surname: this.editEmployeeForm.get('surname')?.value,
        role: this.editEmployeeForm.get('role')?.value,
        daysOff: this.employee.daysOff
      }
      this.employeeService.updateEmployee(dto)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.swal.fireSwalSuccess("Profile updated successfully")
          this.router.navigate(['employees'])
          }),
          catchError( error => {
            this.swal.fireSwalError("Something went wrong while updating profile")
            return throwError(() => error);
          })).subscribe();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
