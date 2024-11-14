import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GetAllHolidayRequestsDto } from '../dtos/holiday-request/get-all-holiday-requests.dto';
import { HolidayRequest } from '../models/holiday-request.model';
import { CreateHolidayRequestDto } from '../dtos/holiday-request/create-holiday-request.dto';

@Injectable({
  providedIn: 'root'
})
export class HolidayRequestService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/employees/holidayRequests`
  
  getAllHolidayRequests(query : string): Observable<GetAllHolidayRequestsDto> {
    return this.http.get<GetAllHolidayRequestsDto>(`${this.baseUrl}${query}`);
  }

  createHolidayRequest(dto : CreateHolidayRequestDto): Observable<HolidayRequest> {
    return this.http.post<HolidayRequest>(`${this.baseUrl}`,dto)
  }

  deleteHolidayRequest(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}` + `/` + `${id}`);
  }
}
