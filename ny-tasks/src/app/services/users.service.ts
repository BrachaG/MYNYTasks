import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient, HttpParams } from "@angular/common/http";
import { userModel } from 'src/models/users.model';
import { AppProxy } from '../app-proxy.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private appProxy: AppProxy, private http: HttpClient) { }


  getUserById(userName: string, code: string): Observable<userModel> {
    {
      let url: string = `api/Users/Get?userName=${userName}&password=${code}`;
       return this.appProxy.get(url)
    }
  }

  getEmailById(): Observable<string> {
    let url: string = `api/Users/GetEmailById`;
    return this.appProxy.get(url);
  }

}
