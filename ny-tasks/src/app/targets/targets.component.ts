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
  constructor(private srv: TargetsService) { }

  ngOnInit() {
    this.getTargets()
  }

  getTargets() {
    this.srv.getTargets().subscribe((res: any) => {
      this.targets = res;
      console.log(this.targets);
    })
  }
}
