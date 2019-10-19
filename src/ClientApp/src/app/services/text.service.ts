import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ApiService } from './api.service';
import { Text, ApiError } from './models';

@Injectable({
  providedIn: 'root'
})
export class TextService {

  constructor(private httpClient: HttpClient, private apiService: ApiService) { }

  public async get(key: string): Promise<Text> {
    try {
      const response = await this.httpClient.get(`/api/microtext/${key}`, { observe: 'response' }).toPromise();
      if (response.status === 200) {
        const stats = response.body as Text;
        return stats;
      }

      throw new ApiError([`Unexpected response: ${response.status}`]);
    } catch (error) {
      this.apiService.throwErrorResponse(error);
      throw error;
    }
  }

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
