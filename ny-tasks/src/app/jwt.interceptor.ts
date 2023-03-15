import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, of, tap, throwError } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  
  constructor(private router: Router) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    localStorage.clear();
    // Get the JWT token from storage
    const token = localStorage.getItem('jwt-token');
    // If there is a token, add it to the authorization header
    if (token) {
      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
        // headers["Authorization"] = `Bearer ${token}`,
      });
      return next.handle(modifiedReq);
    }
    
    return next.handle(req).pipe(
      // Check if the response has a JWT token in the header
      // If it does, update the token in local storage
      // Note that this assumes that the JWT token is stored in the header as 'Authorization'
      // You may need to modify this depending on your server implementation
      // Also note that the token is assumed to be a string, you may need to modify this if it's not
      // For example, if the token is an object containing other information in addition to the token value
      // You'll need to extract the token value from the object
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse && event.headers.has('Authorization')) {
          var tokenToLocalStorage = event.headers.get('Authorization');
          if (tokenToLocalStorage) {
            tokenToLocalStorage = tokenToLocalStorage.replace('Bearer ', '');
            localStorage.setItem('jwt-token', tokenToLocalStorage);
          }
        }
      }),
      catchError((error:HttpErrorResponse) => {
        // If the error is a 401 error, redirect to the login page
        if (error.status === 401) {
          this.router.navigate(['login']);
          return of(new HttpResponse({ status: 200, body: null }));
        }
        return throwError(error);
      })
      );


  }
}
