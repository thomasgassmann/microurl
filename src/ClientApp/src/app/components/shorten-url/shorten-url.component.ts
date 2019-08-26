import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import normalizeUrl from 'normalize-url';
import isUrl from 'is-url-superb';

@Component({
  selector: 'app-shorten-url',
  templateUrl: './shorten-url.component.html',
  styleUrls: ['./shorten-url.component.scss']
})
export class ShortenUrlComponent {
  public url = '';

  public loading = false;
  public errored = false;
  public shortenedUrl: string;

  constructor(private httpClient: HttpClient) { }

  public get validUrl(): boolean {
    return isUrl(this.url);
  }

  public async shorten(): Promise<void> {
    const normalizedUrl = normalizeUrl(this.url);
    this.loading = true;

    this.shortenedUrl = '';
    this.errored = false;
    const response = await this.httpClient
      .post(
        '/api/microurl',
        {
          Url: normalizedUrl
        },
        { observe: 'response' }
      )
      .toPromise();

    if (response.status === 201) {
      this.shortenedUrl = this.getShortenedUrl((response.body as any).key);
      this.url = '';
    } else if (response.status === 400) {
      this.errored = true;
    }

    this.loading = false;
  }

  private getShortenedUrl(key: string): string {
    return normalizeUrl(window.location.host + '/' + key, { forceHttps: true });
  }
}
