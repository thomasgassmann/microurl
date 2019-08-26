import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-url-result',
  templateUrl: './url-result.component.html',
  styleUrls: ['./url-result.component.scss']
})
export class UrlResultComponent {

  @Input() public url: string;

}
