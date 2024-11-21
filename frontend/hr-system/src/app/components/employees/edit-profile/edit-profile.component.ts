import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from '../../../core/services/employee.service';
import { UpdateEmployeeDto } from '../../../core/dtos/employee/update-employee.dto';
import { catchError, Subject, take, takeUntil, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { Employee } from '../../../core/models/employee.model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
  editProfileForm: FormGroup;
  private destroy$ = new Subject<void>();
  employee: Employee | any

  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private router: Router, private swal : AlertService) {
    this.editProfileForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required]
    });
  }

  ngOnInit(){
    this.employeeService.getEmployeeById(history.state.employeeId)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.employee = response
          this.editProfileForm.patchValue({
            name: this.employee.name,
            surname: this.employee.surname
          });
          }),
          catchError( error => {
            this.swal.fireSwalError("Something went wrong while getting employee")
            return throwError(() => error);
          })).subscribe();
  }

  onSubmit() {
    if (this.editProfileForm.valid) {
      const dto: UpdateEmployeeDto = {
        id: this.employee.id,
        name: this.editProfileForm.get('name')?.value,
        surname: this.editProfileForm.get('surname')?.value,
        role: this.employee.role,
        daysOff: this.employee.daysOff
      }
      this.employeeService.updateEmployee(dto)
        .pipe(takeUntil(this.destroy$), tap((response) => {
          this.swal.fireSwalSuccess("Profile updated successfully")
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
