import { Component } from '@angular/core';
import { TargetsService } from '../services/targets.service';
import { Targets } from 'src/models/Targets.model';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-targets',
  templateUrl: './targets.component.html',
  styleUrls: ['./targets.component.scss']
})
export class TargetsComponent {
  targets: Targets[] = [];
  collapsed: boolean = false;
  collapseIcon: string = 'pi pi-angle-up';
  expandIcon: string = 'pi pi-angle-down';
  targetType: number = 0;

  constructor(private srv: TargetsService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe((p: Params) => {
      this.targetType = p['type'];
      console.log(this.targetType + "!");
      this.getTargets(this.targetType);
    })
    
  }

  getTargets(targetType: number) {
    this.srv.getTargets(targetType).subscribe((res: any) => {
      this.targets = res;
      console.log(this.targets);
    })
  }
}
