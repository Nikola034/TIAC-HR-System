import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, switchMap, tap, throwError } from 'rxjs';
import { AccountService } from './account.service';
import { JwtService } from './jwt.service';
import { RefreshTokenDto } from '../dtos/account/refresh-token.dto';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {

  constructor(private accountService : AccountService, private jwtService: JwtService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('access_token');
    if (token) {
      const authRequest = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
      return next.handle(authRequest).pipe(
        catchError((error: HttpErrorResponse) => {
          if(error.status === 401){
            const refresh_token = localStorage.getItem('refresh_token')
            if(refresh_token){
              const dto : RefreshTokenDto = { refreshToken : refresh_token }
              return this.handleExpiredToken(request, next, dto)
            }
            return throwError(() => error);
          }
          return throwError(() => error);
        })
      );
    }

    return next.handle(request);
  }

  private handleExpiredToken(request: HttpRequest<any>, next: HttpHandler, refresh_token : RefreshTokenDto): Observable<HttpEvent<any>> {
    return this.accountService.refreshToken(refresh_token).pipe(
        switchMap( response => {
          this.jwtService.setTokens(response)
          const newAuthRequest = request.clone({
            setHeaders: {
              Authorization: `Bearer ${response.accessToken}`,
            },
          });
          return next.handle(newAuthRequest);
        }),
        catchError( error => {
          console.error('Error intercepted:', error);
          return throwError(() => error);
      }))
  }
}

