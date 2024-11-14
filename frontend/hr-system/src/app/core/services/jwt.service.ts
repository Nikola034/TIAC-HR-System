import { Injectable } from "@angular/core";
import { jwtDecode } from 'jwt-decode';
import { TokensDto } from "../dtos/account/tokens.dto";

@Injectable({
    providedIn: 'root'
  })
  export class JwtService {
    setTokens(tokens: TokensDto) : void {
        localStorage.setItem('access_token',tokens.accessToken)
        localStorage.setItem('refresh_token',tokens.refreshToken)
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

}
