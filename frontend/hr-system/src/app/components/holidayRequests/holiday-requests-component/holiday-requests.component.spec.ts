import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolidayRequestsComponent } from './holiday-requests.component';

describe('HolidayRequestsComponent', () => {
  let component: HolidayRequestsComponent;
  let fixture: ComponentFixture<HolidayRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HolidayRequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HolidayRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
