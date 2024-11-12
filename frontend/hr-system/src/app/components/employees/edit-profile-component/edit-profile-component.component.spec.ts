import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileComponentComponent } from './edit-profile-component.component';

describe('EditProfileComponentComponent', () => {
  let component: EditProfileComponentComponent;
  let fixture: ComponentFixture<EditProfileComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditProfileComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditProfileComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
