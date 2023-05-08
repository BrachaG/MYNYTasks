import { Component, Input, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';
import Chart from 'chart.js/auto';
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
    lQuestions: [],
    lOptions: []
  };
  @Input() question: Question = {
    iQuestionId: 0,
    nvQuestionText: '',
    nvQuestionTypeName: ''
  }
  answers: FilteredAnswers[] = [];
  lables: string[] = [];
  data: number[] = [];

  public chart: any;

  ngOnInit(): void {
    this.sortAnswers();
    this.optionsForAnswer();
    this.createChart();

  }

  sortAnswers() {
    this.result.lResultsForSurveyStudent.forEach(student => {
      student.lAnswers.forEach(answer => {
        if (answer.iQuestionId == this.question.iQuestionId) {
          this.answers.push({ stdName: student.nvFullName, stdBranch: student.nvBranchName, stdAnswer: answer.nvAnswer })
          let option = this.result.lOptions.find(o => o.nvAnswerName == answer.nvAnswer);
          if (option)
            option.sum++;
        }
      })
    })

  }
  optionsForAnswer() {
    this.result.lOptions.forEach(option => {
      if (option.iQuestionId == this.question.iQuestionId)
        this.lables.push(option.nvAnswerName)
      this.data.push(option.sum)
    });
  }
tooltip():string{
  return ""
}
  createChart() {

    this.chart = new Chart("MyChart", {
      type: 'pie',

      data: {
        labels: this.lables,
        datasets: [{
          label: this.tooltip(),
          data: this.data,
          backgroundColor: [
            '#FFA600',
            '#FF59A4',
            '#2F81EF'
          ],
          hoverOffset: 4
        }],
      },
     
      options: {
        aspectRatio: 2.5,
        
      }
    });
  }

}




