import { Component, Input, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';
import { FilteredAnswers } from '../../../../models/filteredAnswers.model';
import * as ChartDataLabels from 'chartjs-plugin-datalabels';
import { Chart, PieController, registerables  } from 'chart.js';

Chart.register(ChartDataLabels, PieController, ...registerables)
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
  dataArr: number[] = [];
  data: any;

  options: any;

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
          this.answers.push({stdId: student.iStudentId.toString(), stdName: student.nvFullName, stdBranch: student.nvBranchName, stdAnswer: answer.nvAnswer, profil: '' })
          let option = this.result.lOptions.find(o => o.nvAnswerName == answer.nvAnswer);
          if (option) {
            if (!option.responders)
              option.responders = []
            option.responders.push(student.nvFullName + ' - ' + student.nvBranchName)
          }
        }
      })
    })

  }
  optionsForAnswer() {
    this.result.lOptions.forEach(option => {
      if (option.iQuestionId == this.question.iQuestionId) {
        this.lables.push(option.nvAnswerName)
      }
      this.dataArr.push(option.responders.length)
    });
  }
  createChart() {

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const getBody = (bodyItem: any, titleLines: any) => {
      const dataNum = this.result.lOptions.find(x => x.nvAnswerName == titleLines)?.responders
      let itemsHtml: any
      if (dataNum)
        itemsHtml = dataNum.map(name => `<div>${name}</div>`).join('');
      return itemsHtml;
    }
  
      this.chart = new Chart("MyChart", {
         
      type: 'pie', 

      data: {
        labels: this.lables,
        datasets: [{
          label: 'My First Dataset',
          data:this.dataArr,
          backgroundColor: [
            '#FF59A4',
            '#2F81EF',
            '#FFA600'
          ],
          borderWidth:0,
          hoverOffset: 4
        }],
      },
     options :{
        plugins: {
          datalabels:{
            display: true,
              formatter: (value, ctx) => {
                const datapoints = ctx.chart.data.datasets[0].data
                const percentage = value / 3 * 100
                return percentage.toFixed(2) + "%";
              },
              color: '#fff',
          },
          legend: {
              position: 'right',
              labels: {
              usePointStyle: true,
              color: textColor,
            }
          },
          tooltip: {
            enabled: false,
            callbacks:{
              
            },
            external: function (context: any) {
              // Tooltip Element
              let tooltipEl = document.getElementById('chartjs-tooltip');
  
              // Create element on first render
              if (!tooltipEl) {
                tooltipEl = document.createElement('div');
                tooltipEl.id = 'chartjs-tooltip';
                document.body.appendChild(tooltipEl);
              }
  
              // Hide if no tooltip
              const tooltipModel = context.tooltip;
              if (tooltipModel.opacity === 0) {
                tooltipEl.style.opacity = '0%';
                return;
              }
  
              // Set Text
              if (tooltipModel.body) {
                const titleLines = tooltipModel.title || [];
                const bodyLines = tooltipModel.body.map((bodyItem: any) => getBody(bodyItem, titleLines));
                let color = tooltipModel.labelColors[0].backgroundColor
                let style = 'background-color:' + color;
                style += '; height: 8px; width:8px; border-radius:50%; margin-left:5px;';
                let innerHtml = '<div class="chartjs-tooltip-section">';
                innerHtml += '<div style="display:flex; align-items: center; position:relative; right: 23%; top:8px;"><div class="chartjs-tooltip-title" style="' + style + '"></div><span style="font-size:16px; font-weight:400" class="chartjs-tooltip-title">' + titleLines[0] + '</span></div>'
                innerHtml += '<div style="position: absolute; width: 170px;height: 0px;left: 5px;top: 36px; border: 0.5px solid #E3E8EE;"> </div>'
                innerHtml += '<ul style="position: relative; top:20px;font-size:12px; font-weight:500"class="chartjs-tooltip-list">';
                innerHtml += '<cdk-virtual-scroll-viewport itemSize="1" class="scroll">'
                innerHtml += bodyLines.join('');
                innerHtml += '</cdk-virtual-scroll-viewport>'
                innerHtml += '</ul></div>';
                tooltipEl.innerHTML = innerHtml;
                innerHtml += '</tbody>';
              }
  
              const position = context.chart.canvas.getBoundingClientRect();
              tooltipEl.style.backgroundColor = "white";
              tooltipEl.style.opacity = '1';
              tooltipEl.style.position = 'absolute';
              tooltipEl.style.width = '180px'
              tooltipEl.style.fontSize = '12px'
              tooltipEl.style.left = position.left + window.pageXOffset + tooltipModel.caretX + 'px';
              tooltipEl.style.top = position.top + window.pageYOffset + tooltipModel.caretY + 'px';
              tooltipEl.style.padding = tooltipModel.padding + 'px ' + tooltipModel.padding + 'px';
              tooltipEl.style.pointerEvents = 'none';
              tooltipEl.style.color = '#1E2959';
              tooltipEl.style.boxShadow = '0px 0px 21px rgba(0, 0, 0, 0.25)';
              tooltipEl.style.borderRadius = '8px';
              tooltipEl.style.minHeight = '70px'
            }
          }
        }
      }
    });
  }
}