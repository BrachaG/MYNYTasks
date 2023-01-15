import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent, SortEvent } from 'primeng/api';
import { Survey } from 'src/models/survey.model';
   


@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
})
export class SurveyComponent implements OnInit{
    surveys:Survey[]=[]

    
    constructor() { }

    selectedSurvey: Survey={
        name: '',
        questions: 0,
        responders: 0,
        endDate: new Date,
        retaliation: '',
        surveyLink: ''
    };


    ngOnInit() {
        this.surveys.push({name:"שופטים - פרסומא תצוגות", questions:3, responders:6, endDate:new Date("2020-10-14"), retaliation:'0 ש"ח', surveyLink:"http://seker.live/#/?p=10"})
        this.surveys.push({name:" שבת שנה ב - פרימה פאלאס", questions:14, responders:5, endDate:new Date("2022-12-30"), retaliation:'10 ש"ח', surveyLink:"http://seker.live/#/?p=10"})
     
    }
}