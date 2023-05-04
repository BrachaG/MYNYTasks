import { Component, Input } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';

@Component({
  selector: 'app-american-question',
  templateUrl: './american-question.component.html',
  styleUrls: ['./american-question.component.scss']
})
export class AmericanQuestionComponent {
  @Input() result :ResultsForSurvey={
    lResultsForSurveyStudent: [],
    lQuestions: []
  };
  @Input() question : Question={
    iQuestionId: 0,
    nvQuestionText: '',
    nvQuestionTypeName: ''
  }
}
