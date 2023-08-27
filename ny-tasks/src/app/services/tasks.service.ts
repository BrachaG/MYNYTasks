import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppProxy } from '../app-proxy.service';
import { Observable } from 'rxjs';
import { Task } from 'src/models/Task.model';

@Injectable({
  providedIn: 'root'
})
export class TasksService {


  constructor(private http: HttpClient, private appProxy: AppProxy) { }
  getTasks(): Observable<Task> {
    {
      let url: string = `api/Task/Get`;
      return this.appProxy.get(url)
    }
  }
}
