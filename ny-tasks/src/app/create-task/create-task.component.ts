import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent {
  @Input()
  sidebarVisible: boolean = false;
  @Output()
  OutputVisible: EventEmitter<boolean> = new EventEmitter<boolean>();
  category = ['1', '2', '3']



}
