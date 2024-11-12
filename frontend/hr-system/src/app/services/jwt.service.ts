import { Injectable } from "@angular/core";
import { jwtDecode } from 'jwt-decode';

@Injectable({
    providedIn: 'root'
  })
  export class JwtService {
    decodeToken(token: string): any {
        try {
          return jwtDecode(token);
        } catch (error) {
          console.error('Invalid token', error);
          return null;
        }
      }
    
      // Check if the token is valid (not expired)
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

    Logout() : void
    {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh_token');
    }

  }
  
