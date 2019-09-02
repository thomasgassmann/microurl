import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StatsKeyStoreService {

  private lastKey: string | null = null;

  public storeLastKey(key: string): void {
    this.lastKey = key;
  }

  public getLastKey(): string | null {
    return this.lastKey;
  }
}
