import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent {
  currentRoute: string = '';
  buttons =

    [['לוח אישי','selfbored', 'pi pi-table'],
    ['יעדים', 'purpose', 'pi pi-send'],
    ['משימות', 'tasks', 'pi pi-pencil'],
    ['משובים', 'surveys', 'pi pi-question-circle']];
  constructor(private router:Router) { }

  navigateTo(route: string) {
    this.currentRoute = route;
    this.router.navigateByUrl(route);
  }
}
