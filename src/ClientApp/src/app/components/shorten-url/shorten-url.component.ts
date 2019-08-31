import { Component } from '@angular/core';
import { ShortenedUrl } from 'src/app/models';
import { ClipboardService, UrlShortenService, SnackbarService } from 'src/app/services';
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
  public optionsExpanded = false;

  public shortenedUrls: ShortenedUrl[] = [];

  public readonly urlForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private urlShortenService: UrlShortenService,
    private clipboardService: ClipboardService,
    private snackbarService: SnackbarService) {
    this.urlForm = this.formBuilder.group({
      url: new FormControl('', [Validators.required, urlValidator]),
      key: new FormControl('', [Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  public get canPaste(): boolean {
    return !!navigator.clipboard;
  }

  public async pasteFromClipboard(): Promise<void> {
    const url = await this.clipboardService.get();
    this.urlField.setValue(url);
  }

  public toggleExpand(): void {
    this.optionsExpanded = !this.optionsExpanded;
  }

  @Toggler<ShortenUrlComponent>('loading')
  public async shorten(): Promise<void> {
    const url = this.urlField.value as string;
    const key = this.keyField.value as string;

    try {
      const response = await this.urlShortenService.shorten(url, key);
      this.shortenedUrls = [{ url: response.url, targetUrl: response.targetUrl }, ...this.shortenedUrls];
      this.urlForm.setValue({
        key: '',
        url: ''
      });
      this.urlForm.markAsUntouched();
    } catch (error) {
      const errorResponse = error as ApiError;
      this.urlForm.setErrors({ tmp: null });
      this.snackbarService.show(errorResponse.message);
    }
  }

  private get urlField(): FormControl {
    return this.urlForm.controls['url'] as FormControl;
  }

  private get keyField(): FormControl {
    return this.urlForm.controls['key'] as FormControl;
  }
}
