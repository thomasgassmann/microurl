import { Injectable } from '@angular/core';

const KEY = 'LAST_STATS_KEY';

@Injectable({
  providedIn: 'root'
})
export class StatsKeyStoreService {

  public storeLastKey(key: string): void {
    localStorage.setItem(KEY, key);
  }

  public getLastKey(): string | null {
    return localStorage.getItem(KEY);
  }
}
