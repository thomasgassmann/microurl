import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TextService {

  constructor(private httpClient: HttpClient, private apiService: ApiService) { }

  public create(language: string, content: string): Promise<string> {
    return this.apiService.handleKeyedCreationResult(this.httpClient
      .post(
        '/api/microtext',
        {
          Language: language,
          Content: content
        },
        { observe: 'response' }
      )
      .toPromise());
  }
}
