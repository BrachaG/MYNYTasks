import { Options } from "./Options.model";
import { Question } from "./Question.model";
import { ResultsForSurveyStudent } from "./ResultsForSurveyStudent.model";

export interface ResultsForSurvey {

    lResultsForSurveyStudent: ResultsForSurveyStudent[];
    lQuestions: Question[];
    lOptions:Options[];
}