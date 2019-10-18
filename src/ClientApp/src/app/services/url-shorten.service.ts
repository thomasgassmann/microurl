import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import isUrl from 'is-url-superb';
import { ApiService } from './api.service';
import { ShortenedUrl, ApiError } from './models';
import normalizeUrl from 'normalize-url';

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

    const resultUrl = await this.apiService.handleKeyedCreationResult(this.httpClient
      .post(
        '/api/microurl',
        {
          Url: normalizedUrl,
          Key: (key || undefined)
        },
        { observe: 'response' }
      )
      .toPromise());
    return {
      targetUrl: normalizedUrl,
      url: resultUrl
    };
  }
}
