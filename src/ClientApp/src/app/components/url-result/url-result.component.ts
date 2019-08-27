import { Component, Input } from '@angular/core';
import { SnackbarService } from 'src/app/services';

@Component({
  selector: 'app-url-result',
  templateUrl: './url-result.component.html',
  styleUrls: ['./url-result.component.scss']
})
export class UrlResultComponent {

  @Input() public url: string;
  @Input() public targetUrl: string;

  constructor(private snackbarService: SnackbarService) {
  }

  public copy(val: string): void {
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    this.snackbarService.show('Copied!');
  }
}
