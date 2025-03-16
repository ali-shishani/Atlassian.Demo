import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {MatIconRegistry, MatIconModule} from '@angular/material/icon';
import {FormControl, Validators, FormsModule, ReactiveFormsModule} from '@angular/forms';

import { GetMoodRatingOptionsResponse, MoodRatingOptionCode, MoodRatingOption, RecordMoodRatingRequest, RecordMoodRatingResponse } from '../../core/models/mood.rating.models';
import { MoodRatingService } from './mood-rating.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  commentFormControl = new FormControl('');

  // todo: implement loading animation
  isLoading: boolean = false;
  isSubmitted: boolean = false;
  isAlreadySubmitted: boolean = false;
  allRatingOptions: MoodRatingOption[] = [];
  selectedRating = 0;

  feelingText = 'Rate your mood!';
  stars = [
    {
      id: 1,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 2,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 3,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 4,
      icon: 'star',
      class: 'star-gray star-hover star'
    }
  ];

constructor(
    private moodRatingService: MoodRatingService,
  ) { 

  }

  ngOnInit(): void {
    
    this.initialise();
  }

  initialise(): void{
    void this.moodRatingService.getMoodRatingOptions().subscribe((data: GetMoodRatingOptionsResponse) => {
      this.allRatingOptions = data.moodRatingOptions;
    });
  }

  isValid(): boolean{
    if(this.isSubmitted || this.selectedRating === 0 || !this.isValidEmail()){
      return false;
    }
    return true;
  }

  isValidEmail(): boolean{
    if(this.emailFormControl.dirty && this.emailFormControl.valid){
      return true;
    }
    return false;
  }

  selectStar(value: number): void{
    if(this.selectedRating === 0){
      this.stars.filter( (star) => {
        if ( star.id <= value){
          star.class = 'star-gold star';
        }else{
          star.class = 'star-gray star';
        }
        return star;
      });
    }

    this.selectedRating = value;
  }

  submit(): void{
    if(this.isValid()){
      let request: RecordMoodRatingRequest = {
        rating: this.selectedRating,
        email: this.emailFormControl.value as string,
        comment: this.commentFormControl.value as string
      };

      void this.moodRatingService.recordMoodRating(request).subscribe((data: RecordMoodRatingResponse) => {
        // todo: implement a proper error dialog
        this.isSubmitted = true;
        this.isAlreadySubmitted = data.alreadyRecorded;
        if(!data.alreadyRecorded){
          alert('Great! all done, we recorded your mood today :)');
        }

      }, 
    error => {
      alert(('oops! something went wrong.'));
      console.log(error);
    });
    }
  }

  onMouseOver(value: number): void {
    if ( this.selectedRating === 0){
      switch(value)
      {
        case 1:
          this.feelingText = 'Not good at all';    
          break;
        case 2:
          this.feelingText = 'A bit “meh”';
          break;
        case 3:
          this.feelingText = 'Pretty good';
          break;
        case 4:
          this.feelingText = 'Feeling great';
          break;
        default:
          break;
      }
    }
  }

  onMouseLeave(value: number): void {
    if ( this.selectedRating === 0){
      this.feelingText = 'Rate your mood!';
    }
  }
}
