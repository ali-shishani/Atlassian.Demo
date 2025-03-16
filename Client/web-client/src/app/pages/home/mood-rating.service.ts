import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, ReplaySubject, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiService } from '../../core/services/api.service';
import { UrlProviderService } from '../../core/url/url.provider';
import { IAPIResponse } from 'src/app/core/models/api.response.models';
import { GetMoodRatingOptionsResponse, RecordMoodRatingRequest, RecordMoodRatingResponse } from 'src/app/core/models/mood.rating.models';

@Injectable({
  providedIn: 'root'
})
export class MoodRatingService {
  isLoadingData = new BehaviorSubject(false);

  constructor(
    private http: HttpClient,
    private urlProviderService: UrlProviderService,
    private apiService: ApiService,
  ) { 

  }

  getMoodRatingOptions(): Observable<GetMoodRatingOptionsResponse> {
    return this.http.get<IAPIResponse<GetMoodRatingOptionsResponse>>(
      this.urlProviderService.getMoodRatingOptionsUrl()).pipe(
        map<IAPIResponse<GetMoodRatingOptionsResponse>, GetMoodRatingOptionsResponse>((response) => {
          this.isLoadingData.next(false);
          return response.data;
        }
        ),
        catchError((error) => this.apiService.handleError(error))
      );
  }

  recordMoodRating(request: RecordMoodRatingRequest): Observable<RecordMoodRatingResponse> {
    return this.http.post<IAPIResponse<RecordMoodRatingResponse>>(
      this.urlProviderService.recordMoodRatingUrl(), request).pipe(
        map<IAPIResponse<RecordMoodRatingResponse>, RecordMoodRatingResponse>((response) => {
          this.isLoadingData.next(false);
          return response.data;
        }
        ),
        catchError((error) => this.apiService.handleError(error))
      );
  }
}
