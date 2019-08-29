import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-titled-card',
  templateUrl: './titled-card.component.html',
  styleUrls: ['./titled-card.component.scss']
})
export class TitledCardComponent {

  @Input() public title = '';

}
