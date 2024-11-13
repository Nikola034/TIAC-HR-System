import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GetAllHolidayRequestsDto } from '../dtos/holiday-request/get-all-holiday-requests.dto';

@Injectable({
  providedIn: 'root'
})
export class HolidayRequestService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/holidayRequests/`
  
  getAllHolidayRequests(query : string): Observable<GetAllHolidayRequestsDto> {
    return this.http.get<GetAllHolidayRequestsDto>(`${this.baseUrl}${query}`);
  }

}
