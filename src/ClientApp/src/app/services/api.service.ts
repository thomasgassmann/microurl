import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ApiError } from './models';
import normalizeUrl from 'normalize-url';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  public async handleKeyedCreationResult(promise: Promise<HttpResponse<Object>>): Promise<string> {
    try {
      const response = await promise;

      if (response.status === 201) {
        return this.getUrlFromkey((response.body as any).key);
      } else {
        throw new ApiError([`Unknown response! ${response.status}: ${response.statusText}`]);
      }
    } catch (error) {
      this.throwErrorResponse(error);
      throw error;
    }
  }

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
      case 409:
        errors.push('Value already exists');
        break;
      default:
        errors.push('Unknown error!');
        break;
    }

    throw new ApiError(errors);
  }

  public getUrlFromkey(key: string): string {
    return normalizeUrl(window.location.host + '/' + key, { forceHttps: true });
  }
}
