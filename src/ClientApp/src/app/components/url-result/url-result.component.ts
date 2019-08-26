import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-url-result',
  templateUrl: './url-result.component.html',
  styleUrls: ['./url-result.component.scss']
})
export class UrlResultComponent {

  @Input() public url: string;

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
  }
}
