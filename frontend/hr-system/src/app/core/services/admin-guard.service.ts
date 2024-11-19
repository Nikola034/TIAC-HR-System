import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router'
import { JwtService } from './jwt.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService implements CanActivate {

  constructor(private jwtService: JwtService, private router: Router) { }

  canActivate(): boolean {
    if (this.jwtService.IsManager()) {
      return true;
    } else {
      this.router.navigate(['projects']);
      return false;
    }
  }

}
