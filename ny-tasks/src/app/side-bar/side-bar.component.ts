import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent {
  @Input()
  sidebarVisible: boolean = false;
  @Output()
  OutputVisible = new EventEmitter<boolean>();
  currentRoute: string = '';
  visibleTypes: boolean = false;
  typesTarget =
    [['שמירת שבת', '/tasks', 'red'],
    ['תפילין', '/tasks', 'yellow'],
    ['צניעות', '/tasks', 'blue'],
    ['תפילה', '/tasks', 'pink']];
  buttons =
    [['לוח אישי', '/selfbored', 'pi pi-table'],
    ['משימות', '/tasks', 'pi pi-pencil'],
    ['משובים', '/surveys', 'pi pi-question-circle'],
    ['סוג יעד', '/targets', 'pi pi-send']];

  constructor(private router: Router, private route: ActivatedRoute) {

    router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    )
      .subscribe(event => {
        this.currentRoute = ((event as NavigationEnd).urlAfterRedirects);
        console.log(this.currentRoute);

      });
  }

  ngOnInit() {
    this.currentRoute = '/surveys';
  }

  navigateTo(route: string) {
    this.router.navigateByUrl(route);
  }

//   visibleTypeButtons(){
//   this.visibleTypes = !this.visibleTypes;
// }

  outPutVisible() {
    this.sidebarVisible = true;
    this.OutputVisible.emit(this.sidebarVisible);
  }
  visibleType(data :boolean){
    this.visibleTypes=data;
  }
}
