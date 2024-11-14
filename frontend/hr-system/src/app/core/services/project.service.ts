
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { GetAllProjectsDto } from '../dtos/project/get-all-project.dto';
import { environment } from '../../../environments/environment';
import { Project } from '../models/project.model';
import { CreateProjectDto } from '../dtos/project/create-project.dto';
import { Employee } from '../models/employee.model';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/projects/`;

  getAllProjects(query : string): Observable<GetAllProjectsDto> {
    return this.http.get<GetAllProjectsDto>(`${this.baseUrl}${query}`);
  }

  getProjectById(id : string): Observable<Project> {
    return this.http.get<Project>(`${this.baseUrl}${id}`);
  }

  getEmployeesOnProject(id : string): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseUrl}allEmployees/${id}`);
  }

  createProject(dto : CreateProjectDto): Observable<Project> {
    return this.http.post<Project>(`${this.baseUrl}`,dto)
  }

  deleteProject(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}${id}`);
  }
}