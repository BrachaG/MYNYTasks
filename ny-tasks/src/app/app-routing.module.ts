import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Sidebar } from 'primeng/sidebar';
import { LoginComponent } from './pages/login/login.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { SideBarComponent } from './side-bar/side-bar.component';

import { TaskComponent } from './pages/task/task.component'
import { TargetsComponent } from './targets/targets.component';


const routes: Routes = [
   { path: 'login', component: LoginComponent },
  { path: 'surveys', component: SurveyComponent },
  { path: 'tasks', component: TaskComponent },
  { path: 'Sidebar', component: SideBarComponent },
  { path: 'targets', component: TargetsComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
