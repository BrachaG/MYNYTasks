import { Component } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent {
  currentRoute: string = '';
  buttons=[['לוח אישי', 'selfbored', 'pi pi-calendar'], ['יעדים', 'pupose', 'pi pi-send'], ['משימות', 'tasks', 'pi pi-pencil'], ['משובים', 'fidback', 'pi pi-question-circle']];
  constructor() { }

  navigateTo(route: string) {
    this.currentRoute = route;
    // this.router.navigateByUrl(route);
  }
}
