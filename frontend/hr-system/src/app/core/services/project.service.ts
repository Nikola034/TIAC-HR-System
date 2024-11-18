
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { GetAllProjectsDto } from '../dtos/project/get-all-project.dto';
import { environment } from '../../../environments/environment';
import { Project } from '../models/project.model';
import { CreateProjectDto } from '../dtos/project/create-project.dto';
import { Employee } from '../models/employee.model';
import { GetAllProjectsForClientDto } from '../dtos/project/get-all-projects-for-client.dto';
import { UpdateProjectDto } from '../dtos/project/update-project.dto';
import { AddOrRemoveEmployeeProjectDto } from '../dtos/employee/add-or-remove-employee-project.dto';
import { DevelopersByProject } from '../dtos/employee/developers-by-project.dto';
import { GetProjectByIdDto } from '../dtos/project/get-project-by-id.dto';
import { GetAllForEmployeeDto } from '../dtos/project/get-all-for-employee.dto';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/projects/`;

  getAllProjects(query : string): Observable<GetAllProjectsDto> {
    return this.http.get<GetAllProjectsDto>(`${this.baseUrl}${query}`);
  }

  getAllForEmployee(employeeId : string): Observable<GetAllForEmployeeDto> {
    return this.http.get<GetAllForEmployeeDto>(`${this.baseUrl}allForEmployee/${employeeId}`);
  }

  getProjectById(id : string): Observable<GetProjectByIdDto> {
    return this.http.get<GetProjectByIdDto>(`${this.baseUrl}${id}`);
  }

  getProjectsByClientId(clientId : string): Observable<GetAllProjectsForClientDto> {
    return this.http.get<GetAllProjectsForClientDto>(`${this.baseUrl}getByClientId/${clientId}`);
  }

  createProject(dto : CreateProjectDto): Observable<Project> {
    return this.http.post<Project>(`${this.baseUrl}`,dto)
  }

  deleteProject(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}${id}`);
  }

  updateProject(dto : UpdateProjectDto): Observable<UpdateProjectDto> {
    return this.http.put<UpdateProjectDto>(`${this.baseUrl}`,dto)
  }

  addEmployeeToProject(dto : AddOrRemoveEmployeeProjectDto): Observable<DevelopersByProject>{
    return this.http.put<DevelopersByProject>(`${this.baseUrl}addToProject`,dto)
  }

  removeEmployeeFromProject(dto : AddOrRemoveEmployeeProjectDto): Observable<DevelopersByProject>{
    return this.http.put<DevelopersByProject>(`${this.baseUrl}removeFromProject`,dto)
  }
}