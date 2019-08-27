import { Component } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import normalizeUrl from 'normalize-url';
import isUrl from 'is-url-superb';
import { ShortenedUrl } from 'src/app/models';

@Component({
  selector: 'app-shorten-url',
  templateUrl: './shorten-url.component.html',
  styleUrls: ['./shorten-url.component.scss']
})
export class ShortenUrlComponent {
  public url = '';

  public loading = false;
  public errored = false;
  public errors: string[] = [];
  public shortenedUrls: ShortenedUrl[] = [];

  constructor(private httpClient: HttpClient) { }

  public get validUrl(): boolean {
    return isUrl(this.url);
  }

  public async shorten(): Promise<void> {
    const normalizedUrl = normalizeUrl(this.url);
    this.loading = true;

    this.errored = false;

    try {
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
        const newUrl = this.getShortenedUrl((response.body as any).key);
        this.shortenedUrls = [{ url: newUrl, targetUrl: normalizedUrl }, ...this.shortenedUrls];
        this.url = '';
      }
    } catch (error) {
      const errorResponse = error as HttpErrorResponse;
      this.errored = true;
      this.errors = [];
      if (errorResponse.status === 400) {
        for (const key of Object.keys(errorResponse.error)) {
          for (const message of errorResponse.error[key]) {
            this.errors.push(`${key}: ${message}`);
          }
        }
      }
    } finally {
      this.loading = false;
    }
  }

  private getShortenedUrl(key: string): string {
    return normalizeUrl(window.location.host + '/' + key, { forceHttps: true });
  }
}
