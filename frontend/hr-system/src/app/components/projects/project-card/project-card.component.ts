import { Component, Input } from '@angular/core';
import { Project } from '../../../model/entities/Project';
import { Client } from '../../../model/entities/Client';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { NgFor } from '@angular/common';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'project-card',
  standalone: true,
  imports: [MatCardModule, CommonModule, NgFor, MatButton],
  templateUrl: './project-card.component.html',
  styleUrl: './project-card.component.css'
})
export class ProjectCardComponent {
  @Input() project!: Project;
}
