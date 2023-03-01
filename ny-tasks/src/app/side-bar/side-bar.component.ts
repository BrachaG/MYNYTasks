import { Component, ViewEncapsulation } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent {
  currentRoute: string = '';
  buttons =

    [['לוח אישי', '/selfbored', 'pi pi-table'],
    ['יעדים', '/purpose', 'pi pi-send'],
    ['משימות', '/tasks', 'pi pi-pencil'],
    ['משובים', '/surveys', 'pi pi-question-circle']];

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
      this.currentRoute='/surveys';
  }

  navigateTo(route: string) {
    this.router.navigateByUrl(route);
  }

}
