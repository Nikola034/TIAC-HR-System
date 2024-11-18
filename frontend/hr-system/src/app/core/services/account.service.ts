import { Injectable } from '@angular/core';
import { LoginDto } from '../dtos/account/login.dto';
import { TokensDto } from '../dtos/account/tokens.dto';
import { query } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { RefreshTokenDto } from '../dtos/account/refresh-token.dto';
import { CreateAccountDto } from '../dtos/employee/create-account.dto';
import { Account } from '../models/account.model';
import { GetAccountByIdsDto } from '../dtos/account/get-account-by-ids.dto';
import { SendAccountIdsDto } from '../dtos/account/send-account-ids.dto';
import { ResetPasswordDto } from '../dtos/account/reset-password.dto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/auth/`;

  createAccount(dto : CreateAccountDto): Observable<Account> {
    return this.http.post<Account>(`${this.baseUrl}register`,dto);
  }

  getAccountById(id: string) : Observable<Account>{
    return this.http.get<Account>(`${this.baseUrl}${id}`);
  }

  getAccountsByIds(dto: SendAccountIdsDto): Observable<GetAccountByIdsDto> {
    return this.http.post<GetAccountByIdsDto>(`${this.baseUrl}allByIds`, dto);
  }

  login(dto : LoginDto): Observable<TokensDto> {
    return this.http.post<TokensDto>(`${this.baseUrl}login`,dto);
  }

  refreshToken(dto : RefreshTokenDto): Observable<TokensDto> {
    return this.http.post<TokensDto>(`${this.baseUrl}refreshToken`,dto);
  }

  sendPasswordResetEmail(email : string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}resetPassword/${email}`)
  }

  resetPassword(dto : ResetPasswordDto): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}resetPassword`,dto)
  }
}
