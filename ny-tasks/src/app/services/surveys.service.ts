import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SurveysService {

  constructor(private http:HttpClient) { }
  getSurveys(): Observable<any> {
    {
        let url: string = `api/Users/GetById?userName=${userName}&password=${code}`;
        return this.http.get<any>(url);
}}
}
