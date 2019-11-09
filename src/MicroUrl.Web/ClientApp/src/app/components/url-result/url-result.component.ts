import { Component, Input } from '@angular/core';
import { SnackbarService, ClipboardService } from 'src/app/services';

@Component({
  selector: 'app-url-result',
  templateUrl: './url-result.component.html',
  styleUrls: ['./url-result.component.scss']
})
export class UrlResultComponent {

  @Input() public url = '';
  @Input() public targetUrl = '';

  constructor(private snackbarService: SnackbarService, private clipboardService: ClipboardService) {
  }

  public async copy(val: string): Promise<void> {
    await this.clipboardService.set(val);
    this.snackbarService.show('Copied!');
  }
}
