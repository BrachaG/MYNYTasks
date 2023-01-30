import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Survey } from 'src/models/survey.model';

@Injectable({
  providedIn: 'root'
})
export class SurveysService {

  constructor(private http:HttpClient) { }
  getSurveys(): Observable<Survey[]> {
    {
        let url: string = `/api/Surveys/Get`;
        return this.http.get<Survey[]>(url);
}}
}
