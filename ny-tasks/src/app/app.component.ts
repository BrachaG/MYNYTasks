import { Component, OnInit, Output } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import * as Chart from 'chart.js';
import * as ChartDataLabels from 'chartjs-plugin-datalabels';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  title = 'trial';
  currentRoute: any;
  chartJs = Chart;
  chartLabelPlugin = ChartDataLabels;

  sidebarVisible: boolean = false;

  constructor(private router: Router) {
    router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    )
      .subscribe(event => {
        this.currentRoute = ((event as NavigationEnd).urlAfterRedirects);
        console.log(this.currentRoute);

      });
  }
  ngOnInit() {
    this.currentRoute = this.router.url;

  }
  onSideBarOutput(data: boolean) {
    this.sidebarVisible = data;
  }
  onCreateTaskOutput(data: boolean) {
    this.sidebarVisible = data;
  }
}