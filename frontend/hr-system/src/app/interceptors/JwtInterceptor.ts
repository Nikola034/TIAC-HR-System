import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { JwtService } from '../core/services/jwt.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor 
{
    constructor(private jwtService: JwtService, private router: Router) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
  {
    let access_token = localStorage.getItem('access_token');
    if (access_token) {
        request = this.addToken(request, access_token);
    }
    return next.handle(request).pipe(
        tap(()=>        console.log('cao')
      ),
        catchError((error) => {
          if (error.status === 401 && access_token) {
            return this.handleTokenExpired(request, next);
          }
  
          return throwError(error);
        })
      );
    }
    private addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
        return request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
          },
        });
      }
    private handleTokenExpired(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       
        return this.jwtService.refreshAccessToken().pipe(
          switchMap(() => {
            const newAccessToken = localStorage.getItem('access_token');
            if(newAccessToken)
            {
                return next.handle(this.addToken(request, newAccessToken));
            }
            return of();
            
          }),
          catchError((error) => {
            console.error('Error handling expired access token:', error);
            this.router.navigate(['/auth/login']);
            return throwError(error);
          })
        );
    }  
  }

