import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpClient, HttpParams } from "@angular/common/http";
@Injectable({
  providedIn: 'root'
})
export class UsersService {
 constructor(private http: HttpClient) { }
  getUserById(userName:string,code:string): Observable<any> {
    {
        let url: string = `api/Users/GetById?userName=${userName}&password=${code}`;
        return this.http.get<any>(url);
}}
}
