import { Component, Input, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';
import Chart from 'chart.js/auto';
import { ResultsForSurveyStudent } from '../../../../models/ResultsForSurveyStudent.model';
import { FilteredAnswers } from '../../../../models/filteredAnswers.model';


@Component({
  selector: 'app-american-question',
  template: '<canvas #myCanvas></canvas>',
  templateUrl: './american-question.component.html',
  styleUrls: ['./american-question.component.scss']
})

export class AmericanQuestionComponent {
  @Input() result: ResultsForSurvey = {
    lResultsForSurveyStudent: [],
    lQuestions: []
  };
  @Input() question: Question = {
    iQuestionId: 0,
    nvQuestionText: '',
    nvQuestionTypeName: ''
  }
  answers:FilteredAnswers[]= [];
  a: number = 0;
  b: number = 0;
  c: number = 0;
  public chart: any;

  ngOnInit(): void { 
    this.sortAnswers();
    this.createChart();
   
  }

  sortAnswers() {
    this.result.lResultsForSurveyStudent.forEach(student => {
      student.lAnswers.forEach(answer => {
        if (answer.iQuestionId == this.question.iQuestionId){
          this.answers.push({ stdName: student.nvFullName, stdBranch: student.nvBranchName, stdAnswer: answer.nvAnswer })
        if (answer.nvAnswer == "כן")
          this.a++;
        if (answer.nvAnswer == "לא")
          this.b++;
        if (answer.nvAnswer == "אולי")
          this.c++;
        } })
    })

  }

  createChart() {

    this.chart = new Chart("MyChart", {
      type: 'pie',

      data: {// values on X-Axis
        labels: ['כן', 'לא', 'אולי'],
        datasets: [{
          label: 'My First Dataset',
          data: [this.a, this.b, this.c],
          backgroundColor: [
            '#FFA600',
            '#FF59A4',
            '#2F81EF'
          ],
          hoverOffset: 4
        }],
      },
      options: {
        aspectRatio: 2.5
      }

    });
  }

}





