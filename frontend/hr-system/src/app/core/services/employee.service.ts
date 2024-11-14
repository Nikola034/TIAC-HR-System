import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Employee } from '../models/employee.model';
import { AllDevelopersDto } from '../dtos/employee/all-developers.dto';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/employees/`;

  getAllDevelopers(): Observable<AllDevelopersDto> {
    return this.http.get<AllDevelopersDto>(`${this.baseUrl}developers`);
  }
}
