import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import normalizeUrl from 'normalize-url';
import isUrl from 'is-url-superb';
import { ApiService } from './api.service';
import { ShortenedUrl, ApiError } from './models';

@Injectable({
  providedIn: 'root'
})
export class UrlShortenService {

  constructor(private httpClient: HttpClient, private apiService: ApiService) { }

  public async shorten(url: string, key?: string): Promise<ShortenedUrl> {
    if (!isUrl(url)) {
      throw new Error('Invalid url');
    }

    const normalizedUrl = normalizeUrl(url);

    try {
      const response = await this.httpClient
        .post(
          '/api/microurl',
          {
            Url: normalizedUrl,
            Key: key
          },
          { observe: 'response' }
        )
        .toPromise();

      if (response.status === 201) {
        const newUrl = this.getShortenedUrl((response.body as any).key);
        return {
          targetUrl: normalizedUrl,
          url: newUrl
        };
      } else {
        throw new ApiError([`Unknown response! ${response.status}: ${response.statusText}`]);
      }
    } catch (error) {
      this.apiService.throwErrorResponse(error);
      throw error;
    }
  }

  private getShortenedUrl(key: string): string {
    return normalizeUrl(window.location.host + '/' + key, { forceHttps: true });
  }
}
