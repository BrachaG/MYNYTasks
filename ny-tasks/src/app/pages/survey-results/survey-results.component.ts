import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Accordion, AccordionModule, AccordionTab } from 'primeng/accordion';
import { ResultsForSurvey } from '../../../models/ResultsForSurvey.model';
import { SurveysService } from '../../services/surveys.service';
import { PaginatorModule } from 'primeng/paginator';
import { Question } from '../../../models/Question.model';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { ViewChild } from '@angular/core';
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
  previousPage:string='prev';
  nextPage:string='next';
  activeIndex: number=0;
  collapsed:boolean = false
  constructor(public surveyService: SurveysService, private route: ActivatedRoute,private paginatorIntl: MatPaginatorIntl) {

    this.paginatorIntl.itemsPerPageLabel = 'Items per page:';
    this.paginatorIntl.nextPageLabel = 'Next page';
    this.paginatorIntl.previousPageLabel = 'Previous page';
  }
  ngOnInit() {
    this.route.params.subscribe((p: Params) => {
      this.id = p['id']
      this.surveyName =p['name']
    });
    this.getResultForSurvey();
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
  openAllTabs() {
    this.collapsed=true
  }
  closeAllTabs() {
    this.collapsed=false
  }
}