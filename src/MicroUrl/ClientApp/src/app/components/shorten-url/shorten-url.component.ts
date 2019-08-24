import { Component, OnInit, ViewChild } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-shorten-url",
  templateUrl: "./shorten-url.component.html",
  styleUrls: ["./shorten-url.component.scss"]
})
export class ShortenUrlComponent {
  public url: string;
  public errored = false;
  public shortenedUrl: string;

  constructor(private httpClient: HttpClient) {}

  public async shorten(): Promise<void> {
    this.shortenedUrl = "";
    this.errored = false;
    const response = await this.httpClient
      .post(
        "/api/microurl",
        {
          Url: this.url
        },
        { observe: "response" }
      )
      .toPromise();

    if (response.status === 201) {
      this.shortenedUrl = this.getShortenedUrl((response.body as any).key);
    } else if (response.status === 400) {
      this.errored = true;
    }
  }

  private getShortenedUrl(key: string): string {
    return "https://" + window.location.host + "/" + key;
  }
}
