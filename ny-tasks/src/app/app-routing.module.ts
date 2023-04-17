import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyResultsComponent } from './pages/survey-results/survey-results.component';
import { SurveyComponent } from './pages/survey/survey.component';

const routes: Routes = [
  { path: "surveys", component: SurveyComponent },
  { path: "surveys-results/:id/:name", component: SurveyResultsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule {
}
