import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-toolbar-logo',
  templateUrl: './toolbar-logo.component.html',
  styleUrls: ['./toolbar-logo.component.scss']
})
export class ToolbarLogoComponent {
  @Output() public toggle: EventEmitter<void> = new EventEmitter<void>();
  @Input() public showMenu = false;

  public toggled(): void {
    this.toggle.emit();
  }
}
