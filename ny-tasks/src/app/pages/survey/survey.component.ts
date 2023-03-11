import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { LazyLoadEvent, SortEvent } from 'primeng/api';
import { SurveysService } from 'src/app/services/surveys.service';
import { Survey } from 'src/models/survey.model';



@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.scss'],
  encapsulation: ViewEncapsulation.None

})
export class SurveyComponent implements OnInit{
    surveys:Survey[]=[];
    sum:number =0;
    constructor(private surveyService:SurveysService,private router:Router) { }

    ngOnInit() { 
       this.getSurveysByUserId();
    }

     getSurveysByUserId(){
           this.surveyService.getSurveys().subscribe((res: any) => {
            this.surveys = res;
            this.sum =this.surveys.length;
          })
         console.log(this.surveys);
         new Date().toDateString
    }
    selectSurvey(iSurveyId:number){

      this.router.navigateByUrl(`surveys-results/${iSurveyId}`);
    }
}
