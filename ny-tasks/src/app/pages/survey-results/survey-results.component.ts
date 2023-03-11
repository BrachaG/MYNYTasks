import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { AccordionModule } from 'primeng/accordion';
import { ResultsForSurvey } from '../../../models/ResultsForSurvey.model';
import { SurveysService } from '../../services/surveys.service';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.scss']
})
export class SurveyResultsComponent implements OnInit {

  id: number = 0;
  result:ResultsForSurvey |null=null;
  constructor(public surveyService: SurveysService, private route: ActivatedRoute) {

  }
  ngOnInit() {
    this.route.params.subscribe((p: Params) => {
      this.id = p['id']
    });
    this.getResultForSurvey();
  }
  getResultForSurvey() {
    this.surveyService.getResultsForSurvey(this.id).subscribe((res: any) => {
      this.result = res;
      console.log(this.result);
      
    })
  }

}
