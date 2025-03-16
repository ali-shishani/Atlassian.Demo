import { ComponentFixture, TestBed } from '@angular/core/testing';
import { noop, of, throwError } from "rxjs";

import { Error } from "../../core/models/error.models";
import { GetMoodRatingOptionsResponse, MoodRatingOptionCode, MoodRatingOption, RecordMoodRatingRequest, RecordMoodRatingResponse } from '../../core/models/mood.rating.models';

import { HomeComponent } from './home.component';

import { LayoutModule } from '../../layout/layout.module';
import { AppModule } from "../../app.module";
import { MoodRatingService } from './mood-rating.service';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let moodRatingService: MoodRatingService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AppModule,
        LayoutModule,
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    moodRatingService = TestBed.inject(MoodRatingService);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('function initialise', () => {
    it('should populate the mood rating selection options', () => {

      let ratingOptions: MoodRatingOption[] = [
        {code: MoodRatingOptionCode.NotGoodAtAll, displayName: 'Not good at all'},
        {code: MoodRatingOptionCode.AbitMeh, displayName: 'A bit “meh”'},
        {code: MoodRatingOptionCode.PrettyGood, displayName: 'Pretty good'},
        {code: MoodRatingOptionCode.FeelingGreat, displayName: 'Feeling great'},
      ];
      let initResult: GetMoodRatingOptionsResponse = {
        moodRatingOptions: ratingOptions,
        errors: [],
      };

      spyOn(moodRatingService, 'getMoodRatingOptions').and.returnValue(of(initResult));
      component.initialise();

      // the submit request should confirm the successful submit
      fixture.detectChanges();
      fixture.whenStable();
      const compiled = fixture.debugElement.nativeElement;
      const resultElement = compiled.querySelectorAll('.star-icon-select');
      fixture.detectChanges();

      // the moodRatingService should call the getMoodRatingOptions function 
      expect(moodRatingService.getMoodRatingOptions).toHaveBeenCalledWith();
      expect(component.allRatingOptions.length).toBe(4);
      expect(resultElement.length).toBe(4);
    });
  });

  describe('function submit', () => {
    it('should record mood rating', () => {
      let email = 'test@test.com';
      let rating = 1;

      component.selectedRating = rating;
      component.emailFormControl.setValue(email);
      component.emailFormControl.markAsDirty();

      let submitResult: RecordMoodRatingResponse = {
        alreadyRecorded: false,
        errors: [],
      };

      spyOn(moodRatingService, 'recordMoodRating').and.returnValue(of(submitResult));
      component.submit();

      let submitRequest: RecordMoodRatingRequest = {
        email: email,
        rating: rating,
        comment: '',
      };

      // the moodRatingService should recieve the correct parameters 
      expect(moodRatingService.recordMoodRating).toHaveBeenCalledWith(submitRequest);

      // the submit request should confirm the successful submit
      fixture.detectChanges();
      fixture.whenStable();
      const compiled = fixture.debugElement.nativeElement;
      const resultElement = compiled.querySelectorAll('.submit-result-text');
      fixture.detectChanges();

      expect(component.isSubmitted).toBeTrue();
      expect(resultElement[0].innerText).toContain('Thank you for recording your mood today! you can leave this page now.');
    });

    it('should handle error when submitting a mood rating', () => {
      let email = 'test@test.com';
      let rating = 1;

      component.selectedRating = rating;
      component.emailFormControl.setValue(email);
      component.emailFormControl.markAsDirty();

      const testError: Error = { 
        code: 500, 
        message: 'Test error',
        description: 'Test error',
      };

      let submitResult: RecordMoodRatingResponse = {
        alreadyRecorded: false,
        errors: [testError],
      };

      spyOn(moodRatingService, 'recordMoodRating').and.returnValue(throwError(testError));
      component.submit();

      // the submit request should confirm the successful submit
      fixture.detectChanges();
      fixture.whenStable();
      const compiled = fixture.debugElement.nativeElement;
      const resultElement = compiled.querySelectorAll('.submit-result-text');
      fixture.detectChanges();

      expect(component.isSubmitted).toBeFalse();
    });

    it('should inform the user of duplicate submit on the same day', () => {
      let email = 'test@test.com';
      let rating = 1;
  
      component.selectedRating = rating;
      component.emailFormControl.setValue(email);
      component.emailFormControl.markAsDirty();
  
      const testError: Error = { 
        code: 500, 
        message: 'Test error',
        description: 'Test error',
      };
  
      let submitResult: RecordMoodRatingResponse = {
        alreadyRecorded: true,
        errors: [testError],
      };
  
      spyOn(moodRatingService, 'recordMoodRating').and.returnValue(of(submitResult));
      component.submit();
  
      // the submit request should confirm the successful submit
      fixture.detectChanges();
      fixture.whenStable();
      const compiled = fixture.debugElement.nativeElement;
      const resultElement = compiled.querySelectorAll('.submit-result-text');
      fixture.detectChanges();
  
      expect(component.isSubmitted).toBeTrue();
      expect(resultElement[0].innerText).toContain('You already submitted your rating today. Please come back tomorrow.');
    });
  });
});

