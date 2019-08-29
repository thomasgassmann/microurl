import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Stats, ApiError, VisitorRanking } from './models';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) { }

  public async getStats(key: string): Promise<Stats> {
    try {
      const response = await this.httpClient.get(`/api/microurl/${key}/stats`, { observe: 'response' }).toPromise();
      if (response.status === 200) {
        const stats = response.body as Stats;
        this.updateDate(stats.allTime);
        stats.recents.map(x => this.updateDate(x));
        return stats;
      }

      throw new ApiError([`Unexpected response: ${response.status}`]);
    } catch (error) {
      this.apiService.throwErrorResponse(error);
      throw error;
    }
  }

  private updateDate(visitors: VisitorRanking) {
    visitors.from = new Date(visitors.from);
    visitors.to = new Date(visitors.to);
  }
}
