import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TargetType } from '../../models/TargetType.model';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor( private http: HttpClient) { }

  getTargetsType(): Observable<TargetType[]>{
    let url: string = `api/Settings/GetTargetsType`;
        return this.http.get<TargetType[]>(url);
  }
}
