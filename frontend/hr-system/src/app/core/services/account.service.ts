import { Injectable } from '@angular/core';
import { LoginDto } from '../dtos/account/login.dto';
import { TokensDto } from '../dtos/account/tokens.dto';
import { query } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { RefreshTokenDto } from '../dtos/account/refresh-token.dto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/auth/`;

  login(dto : LoginDto): Observable<TokensDto> {
    return this.http.post<TokensDto>(`${this.baseUrl}login`,dto);
  }

  refreshToken(dto : RefreshTokenDto): Observable<TokensDto> {
    return this.http.post<TokensDto>(`${this.baseUrl}refreshToken`,dto);
  }
}
