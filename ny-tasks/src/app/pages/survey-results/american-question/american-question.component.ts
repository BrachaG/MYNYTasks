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
          this.answers.push({ stdName: student.nvFullName, stdBranch: student.nvBranchName, stdAnswer: answer.nvAnswer, profil: '' })
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
      this.dataArr.push(option.sum)
    });
  }

  createChart() {

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const getBody = (bodyItem: any) => {
      console.log(this.answers + "answers")
      //const filterA=this.answers.filter()
      const itemsHtml = this.answers.map(name => `<div>${name.stdName} ${name.stdBranch}</div>`).join('');
      //const html = `<ul>${itemsHtml}</ul>`;
      return itemsHtml;
    }
    this.data = {
      labels: this.lables,
      datasets: [
        {
          data: this.dataArr,
          backgroundColor: [
            '#FFA600',
            '#FF59A4',
            '#2F81EF'
          ],
          hoverOffset: 4
        }
      ]
    };

    this.options = {
      plugins: {
        legend: {
          labels: {
            usePointStyle: true,
            color: textColor
          }
        },

        tooltip: {
          callbacks: {

          },
          enabled: false,
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
              const bodyLines = tooltipModel.body.map(getBody);

              let innerHtml = '<div class="chartjs-tooltip-section">';
              innerHtml += '<div class="chartjs-tooltip-title">' + titleLines[0] + '</div>';
              innerHtml += '<ul class="chartjs-tooltip-list">';
              innerHtml += bodyLines.join('');
              innerHtml += '</ul></div>';

              tooltipEl.innerHTML = innerHtml;



              bodyLines.forEach(function (body: any, i: number) {

                const colors = tooltipModel.chart.config._config.data.datasets[0].backgroundColor[i];
                let style = 'background-color:' + colors;
                // style += '; border-color:' + colors.borderColor;
                style += '; border-width: 2px';
                style += '; height: 30px; width:30px'
                const span = '<span style="' + style + '"></span>';
                innerHtml += '<tr><td>' + span + '</td></tr>';
              });
              innerHtml += '</tbody>';
            }

            const position = context.chart.canvas.getBoundingClientRect();
            // const bodyFont = Chart.helpers.toFont(tooltipModel.options.bodyFont);

            // Display, position, and set styles for font
            tooltipEl.style.backgroundColor = "white";
            tooltipEl.style.opacity = '1';
            tooltipEl.style.position = 'absolute';
            tooltipEl.style.left = position.left + window.pageXOffset + tooltipModel.caretX + 'px';
            tooltipEl.style.top = position.top + window.pageYOffset + tooltipModel.caretY + 'px';
            tooltipEl.style.padding = tooltipModel.padding + 'px ' + tooltipModel.padding + 'px';
            tooltipEl.style.pointerEvents = 'none';
            tooltipEl.style.color = '#1E2959';
            tooltipEl.style.boxShadow = '0px 0px 21px rgba(0, 0, 0, 0.25)';
            tooltipEl.style.borderRadius = '8px';
          }
        }
      }
    }
  }
}




