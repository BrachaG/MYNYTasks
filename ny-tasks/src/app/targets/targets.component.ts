import { Component } from '@angular/core';
import { TargetsService } from '../services/targets.service';
import { Targets } from 'src/models/Targets.model';

@Component({
  selector: 'app-targets',
  templateUrl: './targets.component.html',
  styleUrls: ['./targets.component.scss']
})
export class TargetsComponent {
  targets: Targets[]=[];
  collapsed:boolean = false;
  collapseIcon:string = 'pi pi-angle-up';
  expandIcon:string = 'pi pi-angle-down';
  targetType:number=2;
  constructor(private srv: TargetsService) { }

  ngOnInit() {
    this.getTargets(this.targetType)
  }

  getTargets(tagetType:number) {
    this.srv.getTargets(tagetType).subscribe((res: any) => {
      this.targets = res;
      console.log(this.targets);
    })
  }
}
