import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent {
  currentRoute: string = '';
  buttons =

    [['לוח אישי', 'selfbored', 'pi pi-table'],
    ['יעדים', 'purpose', 'pi pi-send'],
    ['משימות', 'tasks', 'pi pi-pencil'],
    ['משובים', 'fidback', 'pi pi-question-circle']];
  constructor() { }

  navigateTo(route: string) {
    this.currentRoute = route;
    // this.router.navigateByUrl(route);
  }
}
