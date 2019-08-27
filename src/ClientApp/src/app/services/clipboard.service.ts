import { Injectable } from '@angular/core';
import * as clipboard from 'clipboard-polyfill/dist/clipboard-polyfill.promise';

@Injectable({
  providedIn: 'root'
})
export class ClipboardService {
  public async set(text: string): Promise<void> {
    await clipboard.writeText(text);
  }

  public async get(): Promise<string> {
    return await navigator.clipboard.readText();
  }
}
