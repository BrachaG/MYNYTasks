import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyResultsComponent } from './pages/survey-results/survey-results.component';
import { Sidebar } from 'primeng/sidebar';
import { LoginComponent } from './pages/login/login.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { SelectBranchComponent } from './pages/select-branch/select-branch.component';
import { AuthGuardService } from './services/auth-guard.service';
import { TargetsComponent } from './targets/targets.component';
import { TaskComponent } from './pages/task/task.component';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'select-branch', component:SelectBranchComponent, canActivate: [AuthGuardService] },
  { path: 'surveys', component: SurveyComponent ,canActivate: [AuthGuardService] },
  { path: 'tasks', component: TaskComponent ,canActivate: [AuthGuardService]},
    { path: 'Sidebar', component: SideBarComponent, canActivate: [AuthGuardService] },
    { path: 'targets', component: TargetsComponent, canActivate: [AuthGuardService]  },
  { path: '', redirectTo: 'surveys', pathMatch: 'full'},
  { path: "surveys-results/:id/:name", component: SurveyResultsComponent ,canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
