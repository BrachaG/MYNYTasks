import { Component } from '@angular/core';
import { TargetsService } from '../services/targets.service';
import { Targets } from 'src/models/Targets.model';

@Component({
  selector: 'app-targets',
  templateUrl: './targets.component.html',
  styleUrls: ['./targets.component.scss']
})
export class TargetsComponent {
  targets:any[]=[];
  constructor(private targetsService:TargetsService ) {}
    
  ngOnInit(){
    this.getTargets()
  }

  getTargets(){
    this.targetsService.getTargets().subscribe((res: Targets[]) => {
     this.targets = res;
   })
  console.log(this.targets.toString());
}
}
