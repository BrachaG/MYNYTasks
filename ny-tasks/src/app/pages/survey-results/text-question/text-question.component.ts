import { Component, Input } from '@angular/core';
import { FilteredAnswers } from '../../../../models/filteredAnswers.model';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';
import { SurveysService } from 'src/app/services/surveys.service';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.scss', '../survey-results.component.scss']
})
export class TextQuestionComponent {

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
  @Input() showImage: boolean = false;

  answers: FilteredAnswers[] = [];
  constructor(public surveyService: SurveysService) {

  }
  ngOnInit() {
    this.sortAnswers();
  }
  sortAnswers() {
    this.result.lResultsForSurveyStudent.forEach(student => {
      student.lAnswers.forEach(answer => {
        if (answer.iQuestionId == this.question.iQuestionId) {
          let arrProfile = student.nvFullName.split(' ')
          this.answers.push({ stdImage: student.image, stdName: student.nvFullName, stdBranch: student.nvBranchName, stdAnswer: answer.nvAnswer, profil: arrProfile[0][0] + '' + arrProfile[1][0] })
        }
      })
    })
  }
}