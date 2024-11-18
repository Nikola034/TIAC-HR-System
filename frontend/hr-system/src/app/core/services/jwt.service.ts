import { Injectable } from "@angular/core";
import { jwtDecode } from 'jwt-decode';
import { TokensDto } from "../dtos/account/tokens.dto";
import { catchError, Observable, tap, throwError } from "rxjs";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
  })
  export class JwtService {

    constructor(private httpClient: HttpClient, private router: Router){}
    setTokens(tokens: TokensDto) : void {
        localStorage.setItem('access_token',tokens.accessToken)
        localStorage.setItem('refresh_token',tokens.refreshToken)
    }

    IsLoged(): boolean {
        const token = this.getToken()
        if (!token) {
            return false; 
        }
        return true; 
    }
    
    getToken(): string | null {
        return localStorage.getItem('access_token');
    }

    decodeToken(token: string): any {
        try {
          return jwtDecode(token);
        } catch (error) {
          console.error('Invalid token', error);
          return null;
        }
      }
    
    isTokenValid(token: string): boolean {
        const decoded = this.decodeToken(token);
        if (!decoded) {
            return false;
        }
        return true;
    }

    IsLoggedIn(): boolean {
        let token = localStorage.getItem('access_token');
        if(token != null)
            return this.isTokenValid(token)
        return false
    }

    IsManager(): boolean {
        return this.getRoleFromToken() == 'Manager'
    }

    IsDeveloper(): boolean {
        return this.getRoleFromToken() == 'Developer'
    }

    getRoleFromToken() : string {
        let token = localStorage.getItem('access_token');
        if(token != null){
            const tokenInfo = this.decodeToken(token);
            const role = tokenInfo.role;
            return role;
        }
        return ""
    }

    getIdFromToken() : string {
        let token = localStorage.getItem('access_token');
        if(token != null){
            const tokenInfo = this.decodeToken(token);
            const id = tokenInfo.id;
            return id;
        }
        return ""
    }

    Logout() : void
    {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh_token');
    }


    refreshAccessToken(): Observable<any> {
        const refreshToken = localStorage.getItem('refresh_token') || '';
        const accessToken = localStorage.getItem('access_token') || '';

        const url = `${environment.apiUrl}/auth/refreshToken`;

        const token: TokensDto = {
        accessToken,
        refreshToken
        };
        return this.httpClient.post<TokensDto>(url, token).pipe(
          tap((response) => {
            
            localStorage.setItem('access_token', response.accessToken);
            localStorage.setItem('refresh_token', response.refreshToken);
          }),
          catchError((error) => {
            this.router.navigate(['/auth/login']);
            console.error('Error refreshing access token:', error);
            return throwError(error);
          })
        );
      }

}
