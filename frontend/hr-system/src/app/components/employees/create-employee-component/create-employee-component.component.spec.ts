import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeComponentComponent } from './create-employee-component.component';

describe('CreateEmployeeComponentComponent', () => {
  let component: CreateEmployeeComponentComponent;
  let fixture: ComponentFixture<CreateEmployeeComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateEmployeeComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateEmployeeComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
