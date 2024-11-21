import { Component, OnInit } from '@angular/core';
import { JwtService } from '../../core/services/jwt.service';
import { Router } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';
import { AlertService } from '../../core/services/alert.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  userRole: string = '';

  constructor(private router: Router, private jwtService: JwtService, private swal: AlertService) {}

  ngOnInit(): void {
      if(this.jwtService.IsLoggedIn()){
        this.userRole = this.jwtService.getRoleFromToken()
        const id = this.jwtService.getIdFromToken()
        const baseUrl = `${environment.apiUrl}/employees/holidayRequests`
        const source = new EventSource(`${baseUrl}/notifications/${id}`);

        source.onmessage = (event) => {
            console.log('Received message:', event.data);
            this.swal.fireSwalSuccess(event.data)
        };

        source.onerror = () => {
            console.error('SSE connection error');
        };

        source.addEventListener('heartbeat', () => {
          console.log('Heartbeat received');
      });
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
