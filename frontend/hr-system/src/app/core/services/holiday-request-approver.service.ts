import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GetAllHolidayRequestApproversByApproverIdDto } from '../dtos/holiday-request/get-all-holiday-request-approves-by-approverid.dto';
import { HolidayRequestApprover } from '../models/holiday-request-approver.model.ts';
import { UpdateHolidayRequestApproverDto } from '../dtos/holiday-request-approver/update-holiday-request-approver.dto'

@Injectable({
  providedIn: 'root'
})
export class HolidayRequestApproverService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/employees/holidayRequestApprovers`
  
  getAllHolidayRequestApproversByApproverId(approverId : string): Observable<GetAllHolidayRequestApproversByApproverIdDto[]> {
    return this.http.get<GetAllHolidayRequestApproversByApproverIdDto[]>(`${this.baseUrl}` + '/byApprover/' + approverId);
  }

  getHolidayRequestApproverByApproverAndRequestId(requestId: string, approverId : string): Observable<HolidayRequestApprover> {
    return this.http.get<HolidayRequestApprover>(`${this.baseUrl}` + '/byApprover/' + requestId + '/' + approverId);
  }

  updateHolidayRequestApprover(dto : UpdateHolidayRequestApproverDto): Observable<HolidayRequestApprover> {
    return this.http.put<HolidayRequestApprover>(`${this.baseUrl}`,dto);
  }
}
