import { TestBed } from '@angular/core/testing';
import { StatiticsService } from './statitics.service';



describe('StatiticsService', () => {
  let service: StatiticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StatiticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
