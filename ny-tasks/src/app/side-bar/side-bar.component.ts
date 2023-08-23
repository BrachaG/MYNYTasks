import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { RouterModule } from '@angular/router';
import { TargetsService } from '../services/targets.service';
import { SettingsService } from '../services/settings.service';
import { TargetType } from '../../models/TargetType.model';
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
  @Input()
  visibleTypes: boolean = false;
  typesTarget :TargetType[]=[];
    // [['שמירת שבת', '/selfbored'],
    // ['מסע אלול', '/selfbored'],
    // ['טהרת המשפחה', '/selfbored'],
    // ['תפילין', '/selfbored'],
    // ['צניעות', '/selfbored'],
    // ['תפילה', '/selfbored'],
    // ['פרוייקט חדש', '/selfbored']];
  buttons =
    [['לוח אישי', '/selfbored', 'pi pi-table'],
    ['משימות', '/tasks', 'pi pi-pencil'],
    ['משובים', '/surveys', 'pi pi-question-circle'],
    ['סוג יעד', '/targets', 'pi pi-send']];
  colors = ['red', 'orange', 'yellow', 'aqua', 'blue', 'pink'];
  constructor(private router: Router, private route: ActivatedRoute, private settingsSrv: SettingsService) {

    router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    )
      .subscribe(event => {
        this.currentRoute = ((event as NavigationEnd).urlAfterRedirects);
        console.log(this.currentRoute);

      });

      this.settingsSrv.getTargetsType().subscribe((res: TargetType[])=>this.typesTarget=res)
  }

  ngOnInit() {
    this.currentRoute = '/surveys';
  }

  navigateTo(route: string) {
    this.router.navigateByUrl(route);
    if (route == '/selfbored' || route == '/tasks' || route == '/surveys')
      this.visibleTypes = false;
    else
      if (route == '/targets')
        this.visibleTypes = !this.visibleTypes;
  }

  outPutVisible() {
    this.sidebarVisible = true;
    this.OutputVisible.emit(this.sidebarVisible);
  }
}
