import { Component } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent {
  currentRoute: string = '';
  buttons=['דשבורד','יעדים','משימות']
  constructor() { }

  navigateTo(route: string) {
    this.currentRoute = route;
    // this.router.navigateByUrl(route);
  }

}
