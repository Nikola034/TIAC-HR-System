import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployeeComponentComponent } from './edit-employee-component.component';

describe('EditEmployeeComponentComponent', () => {
  let component: EditEmployeeComponentComponent;
  let fixture: ComponentFixture<EditEmployeeComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditEmployeeComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditEmployeeComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
