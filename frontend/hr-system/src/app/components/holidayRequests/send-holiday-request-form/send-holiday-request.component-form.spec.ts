import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendHolidayRequestFormComponent } from './send-holiday-request-form.component';

describe('SendHolidayRequestComponent', () => {
  let component: SendHolidayRequestFormComponent;
  let fixture: ComponentFixture<SendHolidayRequestFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SendHolidayRequestFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SendHolidayRequestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
