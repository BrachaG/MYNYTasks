import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-target-buttons',
  templateUrl: './target-buttons.component.html',
  styleUrls: ['./target-buttons.component.scss']
})
export class TargetButtonsComponent {
  @Input() label = '';
  @Output() clicked = new EventEmitter<void>();
  @Input() isActive = false;
  @Input() color = '';
  handleClick() {
    this.clicked.emit();
  }
}
