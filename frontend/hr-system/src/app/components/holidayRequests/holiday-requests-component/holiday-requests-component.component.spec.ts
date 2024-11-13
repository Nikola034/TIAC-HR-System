import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolidayRequestsComponentComponent } from './holiday-requests-component.component';

describe('HolidayRequestsComponentComponent', () => {
  let component: HolidayRequestsComponentComponent;
  let fixture: ComponentFixture<HolidayRequestsComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HolidayRequestsComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HolidayRequestsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
