import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  getServerErrorMessage = (error: HttpErrorResponse): string => {
    switch (error.status) {
      case 404: {
        return `Not Found: ${error.message}`;
      }
      case 403: {
        return `Access Denied: ${error.message}`;
      }
      case 500: {
        return `Internal Server Error: ${error.message}`;
      }
      default: {
        return `Unknown Server Error: ${error.message}`;
      }

    }
  }

  handleError = (error: any): Observable<never> => {
    let errorMsg: string;
    if (error.error instanceof ErrorEvent) {
      errorMsg = 'Something went wrong';
    } else {
      errorMsg = this.getServerErrorMessage(error);
    }
    if (error?.error?.errors?.[0]?.code) {
      errorMsg = error?.error?.errors?.[0]?.description;
    }

    return throwError(() => {new Error(errorMsg)});
  }
}
