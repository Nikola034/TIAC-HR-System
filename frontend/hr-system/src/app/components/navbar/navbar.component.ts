import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { JwtService } from '../../services/jwt.service';
import {MatButtonModule} from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'navbar',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  userRole: string = '';

  constructor(private router: Router, private jwtService: JwtService) {}

  ngOnInit(): void {
      if(this.jwtService.IsLoggedIn()){
        this.userRole = this.jwtService.getRoleFromToken()
      }
  }

  LogOut() : void {
    this.jwtService.Logout();
    this.userRole = ""
  } 

}
