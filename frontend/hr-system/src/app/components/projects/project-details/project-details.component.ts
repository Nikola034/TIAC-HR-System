import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from '../../../core/models/project.model';
import { Employee, EmployeeRole } from '../../../core/models/employee.model';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrl: './project-details.component.css'
})
export class ProjectDetailsComponent implements OnInit{
  projectId!: string;
  project: Project = {id:"id",title:"some title",description:"some description",teamLeadId:"teamlid",client:{id:"id",name:"Clients name",country:"cnt"}};
  employees: Employee[] = [
    {
      id: 'E1',
      name: 'Alice',
      surname: 'Johnson',
      daysOff: 5,
      role: EmployeeRole.Developer,
      accountId: 'A101'
    },
    {
      id: 'E2',
      name: 'Bob',
      surname: 'Smith',
      daysOff: 2,
      role: EmployeeRole.Manager,
      accountId: 'A102'
    },
    {
      id: 'E3',
      name: 'Charlie',
      surname: 'Brown',
      daysOff: 4,
      role: EmployeeRole.Developer,
      accountId: 'A103'
    }
  ];
  teamLead: Employee = {
    id: 'E3',
    name: 'Debil',
    surname: 'Brown',
    daysOff: 4,
    role: EmployeeRole.Developer,
    accountId: 'A103'
  }

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id')!;
  }

  goBack(): void{
    this.router.navigate(['/my-projects'])
  }
}
