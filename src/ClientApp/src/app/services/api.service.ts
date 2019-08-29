import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiError } from './models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  public throwErrorResponse(response: HttpErrorResponse): never {
    const errors: string[] = [];
    switch (response.status) {
      case 400:
        for (const key of Object.keys(response.error)) {
          for (const message of response.error[key]) {
            errors.push(message);
          }
        }
        break;
      case 404:
        errors.push('Resource not found');
        break;
      default:
        errors.push('Unknown error!');
        break;
    }

    throw new ApiError(errors);
  }
}
