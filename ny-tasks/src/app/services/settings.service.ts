import { Injectable } from '@angular/core';
import { AppProxy } from '../app-proxy.service';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http:HttpClient,private appProxy: AppProxy) { }
  getTasksType(): Observable<any> {
    return this.http.get(AppProxy.baseUrl+`api/Settings/GetTasksType`).pipe(
      catchError((error) => {
        console.error('Error getting tasks type:', error);
        return throwError(error);
      })
    );
  }

  addTaskType(name: string): Observable<any> {
    const payload = { name };
    return this.http.post(AppProxy.baseUrl+`api/Settings/AddTaskType?name=${name}`,{}).pipe(
      catchError((error) => {
        console.error('Error adding task type:', error);
        return throwError(error);
      })
    );
  }

  getTargetsType(): Observable<any> {
    
    return this.http.get(AppProxy.baseUrl+`api/Settings/GetTargetsType`)
    // .pipe(
      
    //   catchError((error) => {
    //     console.error('Error getting targets type:', error);
    //     return throwError(error);
    //   })
    // );
  }

  addTargetType(name: string): Observable<any> {
    const payload = { name };
    return this.http.post(AppProxy.baseUrl+`api/Settings/AddTargetType?name=${name}`, {}).pipe(
      catchError((error) => {
        console.error('Error adding target type:', error);
        return throwError(error);
      })
    );
  }
  createBranchGroup(name: string, branches: number[]): Observable<any> {
    return this.http.post(AppProxy.baseUrl+`api/Settings/CreateBranchGroup?name=${name}`, branches).pipe(
      catchError((error) => {
        console.error('Error creating branch group:', error);
        return throwError(error);
      })
    );
  }

  getBranchGroup(): Observable<any> {
    return this.http.get(AppProxy.baseUrl+`api/Settings/GetBranchGroup`).pipe(
      catchError((error) => {
        console.error('Error getting branch group:', error);
        return throwError(error);
      })
    );
  }

  getStatuses(): Observable<any> {
    return this.http.get(AppProxy.baseUrl+`api/Settings/GetStatuses`).pipe(
      catchError((error) => {
        console.error('Error getting statuses:', error);
        return throwError(error);
      })
    );
  }
  getStudentForTask(iBranchId: number): Observable<any> {

    return this.http.get(AppProxy.baseUrl+`api/Task/GetStudentForTask/${iBranchId}`)
    .pipe(
      catchError((error) => {
        console.error('Error getting statuses:', error);
        return throwError(error);
      })
    );
  }

  addStatus(name: string): Observable<any> {
    const payload = { name };
    return this.http.post(AppProxy.baseUrl+`api/Settings/AddStatus?name=${name}/`,{}).pipe(
      catchError((error) => {
        console.error('Error adding status:', error);
        return throwError(error);
      })
    );
  }
  // GetTasksType()  {
  //   {
  //       let url: string = `api/Settings/GetTasksType/`;
  //       return this.appProxy.get(url)

  //   }
  // }
  // AddTaskType( taskTypeName:string | null) {
  //   {
  //     alert(taskTypeName);
  //       let url: string = `api/Settings/AddTaskType?name=${taskTypeName}/`;
  //       return this.appProxy.post(url)
  //   }
  // }
}

