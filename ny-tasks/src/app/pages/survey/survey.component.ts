import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent, SortEvent } from 'primeng/api';
import { SurveysService } from 'src/app/services/surveys.service';
import { Survey } from 'src/models/survey.model';
   


@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
})
export class SurveyComponent implements OnInit{
    surveys:Survey[]=[];
    
    constructor(private surveyService:SurveysService) { }

    ngOnInit() { 
       this.getSurveysByUserId();
    }

    async getSurveysByUserId(){
         await this.surveyService.getSurveys().subscribe((res: any) => {
            this.surveys = res;
          })
         console.log(this.surveys);
    }
}