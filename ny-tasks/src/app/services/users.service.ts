import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from "@angular/common/http";
import { userModel } from 'src/models/users.model';
@Injectable({
  providedIn: 'root'
})
export class UsersService {
 constructor(private http: HttpClient) { }
  getUserById(userName: string, code: string): Observable<userModel> {
    {
        let url: string = `api/Users/Get?userName=${userName}&password=${code}`;
      return this.http.get<userModel>(url);
}}
}
