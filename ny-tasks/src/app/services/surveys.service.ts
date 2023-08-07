import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Survey } from '../../models/survey.model';
import { ResultsForSurvey } from '../../models/ResultsForSurvey.model';

@Injectable({
  providedIn: 'root'
})
export class SurveysService {

  constructor(private http:HttpClient) { }
  getSurveys( branch:string | null): Observable<Survey[]> {
    {
        let url: string = `api/Surveys/Get?branchId=${branch}`;
        return this.http.get<Survey[]>(url);
    }
  }

  getResultsForSurvey(id: number, branch:string | null):Observable<ResultsForSurvey>{
    let url: string = `api/Surveys/Get/${id}?branchId=${branch}`;
    return this.http.get<ResultsForSurvey>(url);
  }
}
