import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Employee } from '../models/employee.model';
import { AllDevelopersDto } from '../dtos/employee/all-developers.dto';
import { AllEmployeesDto } from '../dtos/employee/all-employees.dto';
import { CreateEmployeeDto } from '../dtos/employee/create-employee.dto';
import { UpdateEmployeeDto } from '../dtos/employee/update-employee.dto';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/employees/`;

  getAllDevelopers(): Observable<AllDevelopersDto> {
    return this.http.get<AllDevelopersDto>(`${this.baseUrl}developers`);
  }

  getAllEmployees(query: string): Observable<AllEmployeesDto> {
    return this.http.get<AllEmployeesDto>(`${this.baseUrl}${query}`);
  }

  createEmployee(dto : CreateEmployeeDto): Observable<Employee> {
    return this.http.post<Employee>(`${this.baseUrl}`,dto)
  }

  deleteEmployee(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}${id}`);
  }

  updateEmployee(dto : UpdateEmployeeDto): Observable<UpdateEmployeeDto> {
    return this.http.put<UpdateEmployeeDto>(`${this.baseUrl}`,dto)
  }
}
