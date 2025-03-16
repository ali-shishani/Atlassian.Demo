
import { Error } from "./error.models";

export enum MoodRatingOptionCode {
    NotGoodAtAll = 1,
    AbitMeh = 2,
    PrettyGood = 3,
    FeelingGreat = 4,
}

export interface MoodRatingOption {
    code: MoodRatingOptionCode;
    displayName: string;
}

export interface GetMoodRatingOptionsResponse {
    moodRatingOptions: MoodRatingOption[];
    errors: Error[];
}

export interface RecordMoodRatingRequest {
    rating: number;
    email: string;
    comment: string;
}

export interface RecordMoodRatingResponse {
    alreadyRecorded: boolean;
    errors: Error[];
}

