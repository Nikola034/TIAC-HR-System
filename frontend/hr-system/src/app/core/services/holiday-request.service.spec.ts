import { TestBed } from '@angular/core/testing';

import { HolidayRequestService } from './holiday-request.service';

describe('HolidayRequestService', () => {
  let service: HolidayRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HolidayRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
