import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Targets } from 'src/models/Targets.model';
import { AppProxy } from '../app-proxy.service';

@Injectable({
  providedIn: 'root'
})
export class TargetsService {

  constructor(private http:HttpClient,private appProxy: AppProxy,) { }
  getTargets(): Observable<Targets[]> {
    {
        let url: string = `api/Targets/Get`;
        // return this.http.get<Targets[]>(url);
        return this.appProxy.get(url)

}}
}
