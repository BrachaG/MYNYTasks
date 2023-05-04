import { Component, Input } from '@angular/core';
import { Question } from '../../../../models/Question.model';
import { ResultsForSurvey } from '../../../../models/ResultsForSurvey.model';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.scss','../survey-results.component.scss']
})
export class TextQuestionComponent {

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
