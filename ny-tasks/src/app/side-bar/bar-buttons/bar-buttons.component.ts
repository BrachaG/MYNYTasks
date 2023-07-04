
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
  @Input() isTarget=false
  @Output() visibleTypesOutput=new EventEmitter<boolean>();
  visibleTypes:boolean=false;
  handleClick() {
    this.clicked.emit();
  }
  visibleTypeButtons(){
    this.visibleTypes=!this.visibleTypes
    this.visibleTypesOutput.emit(this.visibleTypes);
     }
    
}
