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
  originTask:string="button";
  isLoggedIn = false;

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
       // Check if the user is logged in by reading from localStorage
       const loginStatus = localStorage.getItem('isLoggedIn');
       if (loginStatus && loginStatus === 'true') {
         this.isLoggedIn = true;}

  }
  onSideBarOutput(data: boolean) {
    this.sidebarVisible = data;
  }
  onCreateTaskOutput(data: boolean) {
    this.sidebarVisible = data;
  }


  
  setUserLoggedIn(status: boolean) {
    this.isLoggedIn = status;
    localStorage.setItem('isLoggedIn', status.toString());
  }
}