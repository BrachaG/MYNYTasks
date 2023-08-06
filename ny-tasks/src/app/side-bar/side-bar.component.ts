import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent {
 @Input()
 sidebarVisible:boolean=false;
 @Output()
 OutputVisible=new EventEmitter<boolean>();
  currentRoute: string = '';
  buttons =

    [['לוח אישי', '/selfbored', 'pi pi-table'],
    ['יעדים', '/targets', 'pi pi-send'],
    ['משימות', '/tasks', 'pi pi-pencil'],
    ['משובים', '/surveys', 'pi pi-question-circle']];

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
      this.currentRoute='/surveys';
  }

  navigateTo(route: string) {
    this.router.navigateByUrl(route);
  }
  outPutVisible(){
    this.sidebarVisible=true;
    this.OutputVisible.emit(this.sidebarVisible);
  }
}
