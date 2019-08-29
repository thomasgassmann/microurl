import { Component } from '@angular/core';
import { ShortenedUrl } from 'src/app/models';
import { ClipboardService, UrlShortenService } from 'src/app/services';
import { ApiError } from 'src/app/services/models';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { urlValidator } from 'src/app/validators';
import { Toggler } from 'src/app/common';

@Component({
  selector: 'app-shorten-url',
  templateUrl: './shorten-url.component.html',
  styleUrls: ['./shorten-url.component.scss']
})
export class ShortenUrlComponent {
  public loading = false;

  public shortenedUrls: ShortenedUrl[] = [];

  public readonly urlForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private urlShortenService: UrlShortenService,
    private clipboardService: ClipboardService) {
    this.urlForm = this.formBuilder.group({
      url: new FormControl('', [Validators.required, urlValidator])
    });
  }

  public get canPaste(): boolean {
    return !!navigator.clipboard;
  }

  public async pasteFromClipboard(): Promise<void> {
    const url = await this.clipboardService.get();
    this.urlField.setValue(url);
  }

  @Toggler<ShortenUrlComponent>('loading')
  public async shorten(): Promise<void> {
    const url = this.urlField.value as string;

    try {
      const response = await this.urlShortenService.shorten(url);
      this.shortenedUrls = [{ url: response.url, targetUrl: response.targetUrl }, ...this.shortenedUrls];
      this.urlField.setValue('');
      this.urlField.markAsUntouched();
    } catch (error) {
      const errorResponse = error as ApiError;
      this.urlField.setErrors({ server: errorResponse.message });
    }
  }

  private get urlField(): FormControl {
    return this.urlForm.controls['url'] as FormControl;
  }
}
