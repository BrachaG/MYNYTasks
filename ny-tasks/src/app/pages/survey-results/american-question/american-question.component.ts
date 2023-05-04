import { Component, Input, OnInit } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-american-question',
  // template: '<canvas id="myChart"></canvas>',
  templateUrl: './american-question.component.html',
  styleUrls: ['./american-question.component.scss']
})
 
export class AmericanQuestionComponent implements OnInit {
  @Input() result: ResultsForSurvey = {
    lResultsForSurveyStudent: [],
    lQuestions: []
  };
  @Input() question: Question = {
    iQuestionId: 0,
    nvQuestionText: '',
    nvQuestionTypeName: ''
  }

  ngOnInit() {
    // // const ctx = document.getElementById('myChart');
    // const data: ChartData = {
    //   labels: ['Red', 'Blue', 'Yellow'],
    //   datasets: [
    //     {
    //       data: [10, 20, 30],
    //       backgroundColor: ['#ff6384', '#36a2eb', '#ffce56'],
    //     },
    //   ],
    // };

    // const options = {
    //   responsive: true,
    // };

    // const pieChart = new Chart('myChart', {
    //   type: 'pie',
    //   data,
    //   options,
    // });
  }
}
interface ChartData {
  labels: string[];
  datasets: {
    data: number[];
    backgroundColor: string[];
  }[];
}




