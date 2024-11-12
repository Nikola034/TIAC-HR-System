import { Component, OnInit } from '@angular/core';
import { JwtService } from '../../core/services/jwt.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  userRole: string = '';

  constructor(private router: Router, private jwtService: JwtService) {}

  ngOnInit(): void {
      if(this.jwtService.IsLoggedIn()){
        this.userRole = this.jwtService.getRoleFromToken()
      }
  }

  logOut() : void {
    this.jwtService.Logout();
    this.userRole = ""
  } 

}
