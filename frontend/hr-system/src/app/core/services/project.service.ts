
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { GetAllProjectsDto } from '../dtos/project/get-all-project.dto';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/projects/`
  getAllProjects(query : string): Observable<GetAllProjectsDto> {
    return this.http.get<GetAllProjectsDto>(`${this.baseUrl}${query}`);
  }
}