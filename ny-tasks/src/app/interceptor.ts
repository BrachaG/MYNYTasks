import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

export class JwtInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Get the JWT token from storage
    const token = localStorage.getItem('jwt-token');
    // If there is a token, add it to the authorization header
    if (token) {
      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer' + token),
      });
      return next.handle(modifiedReq);
    }
    // If there is no token, just pass the request along
    return next.handle(req);
  }
}
