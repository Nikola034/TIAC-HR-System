import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GetAllHolidayRequestApproversByApproverIdDto } from '../dtos/holiday-request/get-all-holiday-request-approves-by-approverid.dto';

@Injectable({
  providedIn: 'root'
})
export class HolidayRequestApproverService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/holidayRequestApprovers/`
  
  getAllHolidayRequestApproversByApproverId(query : string): Observable<GetAllHolidayRequestApproversByApproverIdDto> {
    return this.http.get<GetAllHolidayRequestApproversByApproverIdDto>(`${this.baseUrl}${query}`);
  }

}
