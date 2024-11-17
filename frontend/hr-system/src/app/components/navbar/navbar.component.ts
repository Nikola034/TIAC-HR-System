import { Component, OnInit } from '@angular/core';
import { JwtService } from '../../core/services/jwt.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  userRole: string = '';

  constructor(private router: Router, private jwtService: JwtService, private employeeService: EmployeeService) {}

  ngOnInit(): void {
      if(this.jwtService.IsLoggedIn()){
        this.userRole = this.jwtService.getRoleFromToken()
      }
  }

  editProfile(): void{
    this.router.navigate(['profile'], { state: { employeeId: this.jwtService.getIdFromToken()} })
  }

  logOut() : void {
    this.jwtService.Logout();
    this.userRole = ""
  } 

}
