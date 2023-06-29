import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    // Check if user is authenticated or meets certain conditions
    const isAuthenticated = localStorage.getItem('jwt-token');

    if (isAuthenticated) {
      return true; // Allow navigation
    } else {
      // Redirect to a different route or show an error message
      this.router.navigate(['/login']);
      return false; // Block navigation
    }
  }
}
