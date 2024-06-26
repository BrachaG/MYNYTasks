import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { SettingsService } from '../../services/settings.service';
import { TargetsService } from '../../services/targets.service';


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
  @Input() type=0;
 
  constructor(private router: Router) {}
   
  ngOnInit() {

   
  }
  
  handleClick(type:number) {
   // this.clicked.emit();
    this.router.navigateByUrl(`targets/${type}`)
  }
}
