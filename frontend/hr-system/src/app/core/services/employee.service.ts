import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Employee } from '../models/employee.model';
import { AllDevelopersDto } from '../dtos/employee/all-developers.dto';
import { AllEmployeesDto } from '../dtos/employee/all-employees.dto';
import { CreateEmployeeDto } from '../dtos/employee/create-employee.dto';
import { UpdateEmployeeDto } from '../dtos/employee/update-employee.dto';
import { DaysOffReportDto } from '../dtos/employee/days-off-report.dto';

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

  getAllEmployeesOnProject(projectId: string): Observable<AllEmployeesDto> {
    return this.http.get<AllEmployeesDto>(`${this.baseUrl}allEmployees/${projectId}`);
  }
  
  getEmployeeById(id: string | undefined): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseUrl}${id}`);
  }

  getDaysOffForEmployee(id: string | undefined): Observable<DaysOffReportDto> {
    return this.http.get<DaysOffReportDto>(`${this.baseUrl}daysOff/${id}`);
  }

  getEmployeeByAccountId(accountId: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseUrl}getByAccountId/${accountId}`);
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
