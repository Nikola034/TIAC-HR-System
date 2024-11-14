import { TestBed } from '@angular/core/testing';
import { HolidayRequestApproverService } from './holiday-request-approver.service';


describe('HolidayRequestApproverService', () => {
  let service: HolidayRequestApproverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HolidayRequestApproverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
