import { Component, OnInit, ViewEncapsulation } from '@angular/core';
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
    filterText: string = '';
    filteredSurveys: Survey[] =[];

    constructor(private surveyService:SurveysService) { }

    ngOnInit() { 
       this.getSurveysByUserId();
       this.applyFilter();
    }

     getSurveysByUserId(){
           this.surveyService.getSurveys().subscribe((res: any) => {
            this.surveys = res;
            this.filteredSurveys = res;
            this.sum =this.surveys.length;
          })
         console.log(this.surveys);
         new Date().toDateString
    }
    applyFilter() {
      console.log(this.filterText);
      
  const filterValue = this.filterText.toLowerCase();
  this.filteredSurveys = this.surveys.filter((survey) => {
    // Apply your desired filtering logic based on the survey properties
    return (
      survey.nvSurveyName.toLowerCase().includes(filterValue) ||
      survey.iRespondentsCount.toString().includes(filterValue) ||
      survey.iQuestionCount.toString().includes(filterValue) ||
      survey.nvLink.toLowerCase().includes(filterValue)
    );
  });
}

}
