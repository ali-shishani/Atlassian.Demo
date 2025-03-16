import { Injectable } from '@angular/core';
import urlcat from 'urlcat';
import { apiUrl } from '../settings/app.config';

@Injectable({
    providedIn: 'root',
  })
  export class UrlProviderService {
    constructor() {
    }
  
    getMoodRatingOptionsUrl = (): string => {
      return urlcat(apiUrl || '', '/api/MoodRating/GetMoodRatingOptions');
    }

    recordMoodRatingUrl = (): string => {
      return urlcat(apiUrl || '', '/api/MoodRating/RecordMoodRating');
    }
  }
  