import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TargetsService {

  constructor(private http:HttpClient) { }
  getTargets(): Observable<any[]> {
    {
      let userId = 2;
      let status = 1;
        let url: string = `api/Targets/Get`;
        return this.http.get<any[]>(url);
}}
}
