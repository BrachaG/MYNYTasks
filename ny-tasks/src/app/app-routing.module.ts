import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyResultsComponent } from './pages/survey-results/survey-results.component';
import { Sidebar } from 'primeng/sidebar';
import { LoginComponent } from './pages/login/login.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { TasksComponent } from './tasks/tasks.component';
import { SelectBranchComponent } from './pages/select-branch/select-branch.component';

const routes: Routes = [
   { path: 'login', component: LoginComponent },
   {path: 'select-branch',component:SelectBranchComponent},
  { path: 'surveys', component: SurveyComponent },
  { path: 'tasks', component: TasksComponent },
  { path: 'Sidebar', component: SideBarComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: "surveys-results/:id/:name", component: SurveyResultsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
