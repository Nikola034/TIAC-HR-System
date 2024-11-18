import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GetAllHolidayRequestsDto } from '../dtos/holiday-request/get-all-holiday-requests.dto';
import { HolidayRequest } from '../models/holiday-request.model';
import { CreateHolidayRequestDto } from '../dtos/holiday-request/create-holiday-request.dto';
import { GetAllHolidayRequestsBySenderDto } from '../dtos/holiday-request/get-all-holiday-requests-bysender.dto';
import { GetAllHolidayRequestsToApproveDto } from '../dtos/holiday-request/get-all-holiday-requests-toapprove.dto';

@Injectable({
  providedIn: 'root'
})
export class HolidayRequestService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/employees/holidayRequests`
  
  getAllHolidayRequests(query : string): Observable<GetAllHolidayRequestsDto> {
    return this.http.get<GetAllHolidayRequestsDto>(`${this.baseUrl}${query}`);
  }

  getAllHolidayRequestsToApprove(approverId: string): Observable<GetAllHolidayRequestsToApproveDto> {
    return this.http.get<GetAllHolidayRequestsToApproveDto>(`${this.baseUrl}/toApprove/${approverId}`);
  }

  getAllHolidayRequestsBySenderId(senderId : string, query: string): Observable<GetAllHolidayRequestsBySenderDto> {
    return this.http.get<GetAllHolidayRequestsBySenderDto>(`${this.baseUrl}/bySender/${senderId}${query}`);
  }

  getHolidayRequestById(id : string): Observable<HolidayRequest> {
    return this.http.get<HolidayRequest>(`${this.baseUrl}/${id}`);
  }

  createHolidayRequest(dto : CreateHolidayRequestDto): Observable<HolidayRequest> {
    return this.http.post<HolidayRequest>(`${this.baseUrl}`,dto)
  }

  deleteHolidayRequest(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}` + `/` + `${id}`);
  }
}
