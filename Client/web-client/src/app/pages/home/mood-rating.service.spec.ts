import { TestBed } from '@angular/core/testing';

import { MoodRatingService } from './mood-rating.service';

import { LayoutModule } from '../../layout/layout.module';
import { AppModule } from "../../app.module";

describe('MoodRatingService', () => {
  let service: MoodRatingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
          imports: [
            AppModule,
            LayoutModule,
          ],
        })
        .compileComponents();
    
    service = TestBed.inject(MoodRatingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
