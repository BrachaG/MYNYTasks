import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { tap, catchError } from 'rxjs/operators';
// import { MessageService } from "./service/message.service";
// import { BehaviorSubject } from 'rxjs/BehaviorSubject';
@Injectable()
export class AppProxy {
  res: any;
  public static baseUrl:string;
  // public static baseUrlFiles:string;

  private isLoad = false;
  
  constructor(private http: HttpClient) {
    switch (location.hostname) {
      case 'localhost':
        AppProxy.baseUrl = "https://localhost:7149/";
        break;
      case 'qa.webit-track.com':
        AppProxy.baseUrl = "http://qa.webit-track.com/MYNYNewWS/";
        break;
      case '10.0.0.230':
        AppProxy.baseUrl = "http://10.0.0.230/MYNY/Service/";
        break;
    }
  }


  public post(url: string, data: any = {}): Observable<any> {    
    this.isLoad = true;
    console.log(url+":",data);
    console.log(JSON.stringify(data));
    return this.http.post(`${AppProxy.baseUrl}${url}`, "55")
    .pipe(
      tap(data => {
          this.res = data;	
            this.isLoad = false	
            return this.convertData(data, false);
        }))
      
  }
  public put(url: string, data: any = {}): Observable<any> {    
    let a=this.convertData(data, true);
    this.isLoad = true;
    console.log(url+":",data);
    console.log(JSON.stringify(data));
    return this.http.put(`${AppProxy.baseUrl}${url}`,    Object.values(this.convertData(data, true)))
    .pipe(
      tap(data => {
          this.res = data;	
            this.isLoad = false	
            return this.convertData(data, false);
        }))
      
  }

  // public get(url: string): Promise<any> {
  //   return this.http
  //     .get(`${AppProxy.baseUrl}${url}`)
  //     .toPromise()
  //     .then(result => { return this.convertData(result, false) })
  //     .catch(error => { return Promise.reject(error) });
  // }

  public get(url: string): Observable<any> {
    return this.http
      .get(`${AppProxy.baseUrl}${url}`)
      .pipe(
        tap(data => {
          
            this.res = data;	
              return this.convertData(data, false);
          }))

  }


  private convertData(data:any, isPost:boolean) {
    if (!(data instanceof Object) || data instanceof Date) {
      let prop = data;
      // parse string date
      if (
        isPost === false &&
        (prop instanceof String || typeof prop === "string") &&
        prop.indexOf("/Date(") > -1
      ) {
        let date = prop.substring(
          prop.indexOf("(") + 1,
          prop.indexOf("+") != -1 ? prop.indexOf("+") : prop.indexOf(")")
        );
        prop = new Date(parseInt(date));
      } else if (isPost && prop instanceof Date) {
        // convert to string date
        let d = Date.UTC(
          prop.getFullYear(),
          prop.getMonth(),
          prop.getDate(),
          prop.getHours(),
          prop.getMinutes()
        );
        prop =
          d.toString() === "NaN" || Number.isNaN(d)
            ? null
            : "/Date(" +
            Date.UTC(
              prop.getFullYear(),
              prop.getMonth(),
              prop.getDate(),
              prop.getHours(),
              prop.getMinutes()
            ) +
            ")/";
      }
      return prop;
    }

    // parse array / object
    let isArr = data instanceof Array;
    let arrayData: any[] = [];
    let objectData :any= {};

    if (data) {
      Object.keys(data).forEach(key => {
        // dictionary
        if (
          !isPost &&
          isArr &&
          data[key] &&
          Object.keys(data[key]).length === 2 &&
          data[key].Key &&
          data[key].Value != null
        ) {
          arrayData[data[key].Key] = this.convertData(data[key].Value, isPost);
        } else if (isArr) {
          // array
          arrayData.push(this.convertData(data[key], isPost));
        } else {
          // object
          objectData[key] = this.convertData(data[key], isPost);
        }
      });
    }
    return isArr ? arrayData : objectData;
  }
  public getIsLoad() {
    return this.isLoad;
  }
}