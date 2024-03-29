import { Component } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-bar-buttons',
  templateUrl: './bar-buttons.component.html',
  styleUrls: ['./bar-buttons.component.scss'],

})
export class BarButtonsComponent {
  @Input() label = '';
  @Output() clicked = new EventEmitter<void>();
  @Input() isActive = false;
  @Input() icon = '';
  @Input() isTarget = false
  @Input() visibleTypes: boolean = false;

  handleClick() {
    this.clicked.emit();
  }
}
