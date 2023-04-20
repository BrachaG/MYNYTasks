import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { AccordionModule } from 'primeng/accordion';
import { ResultsForSurvey } from '../../../models/ResultsForSurvey.model';
import { SurveysService } from '../../services/surveys.service';
import { PaginatorModule } from 'primeng/paginator';
import { Question } from '../../../models/Question.model';
@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.scss']
})

export class SurveyResultsComponent implements OnInit {
  expandIcon:string = 'pi pi-angle-down';
  collapseIcon:string = 'pi pi-angle-up';
  id: number = 0;
  surveyName: string='';
  result!: ResultsForSurvey;
  sum:number=0;
  currentPage : number = 0;
  first:number =0;
  rows: number = 10;
  pages:any[]=[];
  constructor(public surveyService: SurveysService, private route: ActivatedRoute) {

  }
  ngOnInit() {
    this.route.params.subscribe((p: Params) => {
      this.id = p['id']
      this.surveyName =p['name']
    });
    this.getResultForSurvey();

  //   const questions = this.result.lQuestions;
  // for (let i = 0; i < questions.length; i += 10) {
  //   const page = questions.slice(i, i + 10);
  //   this.pages.push(page);
  // }
}
  getResultForSurvey() {
    this.surveyService.getResultsForSurvey(this.id).subscribe((res: any) => {
      this.result = res;
      console.log(this.result);
      this.sum=this.result.lQuestions.length;
    })
  }
 

  onPageChange(event:any) {
      this.first  = event.first;
  }
}
