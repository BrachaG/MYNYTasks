import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable, of, tap, throwError } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }
intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  // Get the JWT token from storage
  const token = localStorage.getItem('jwt-token');

  // If there is a token, add it to the authorization header
  if (token) {
    const modifiedReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`),
    });
    req = modifiedReq;
  }

  return next.handle(req).pipe(
    map((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse && event.headers.has('Authorization')) {
        var tokenToLocalStorage = event.headers.get('Authorization');
        if (tokenToLocalStorage) {
          tokenToLocalStorage = tokenToLocalStorage.replace('Bearer ', '');
          localStorage.setItem('jwt-token', tokenToLocalStorage);
        }
      }
      return event;
    }),
    catchError((error: HttpErrorResponse) => {
      // If the error is a 401 error, redirect to the login page
      if (error.status === 401) {
        alert("you need to relogin");
        this.router.navigate(['login']);
        return of(new HttpResponse({ status: 200, body: null }));
      }
      return throwError(error);
    })
  );
}

}

