import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { SignInResponse } from '../services/user/sign-in/models/response/sign-in-response';
import { CookieService } from 'ngx-cookie-service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate, CanActivateChild  {
  readonly _tokenKey = 'gettk';
  readonly _userNameKey = 'userName';

    constructor(
      private router: Router,
      private cookieService: CookieService
    ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let canActivate = false;

    if (this.checkUserIsLoggedIn()) {
        canActivate = true;
    }
    else {
      this.router.navigate(['/login']);
    }

    return canActivate;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):  boolean {
    return this.canActivate(route, state);
}

  signIn(response: SignInResponse) {
    const expires: Date = new Date();
    expires.setSeconds( response.expireSeconds );

    this.cookieService.set(this._tokenKey, response.token, expires);
    localStorage.setItem(this._userNameKey, response.name);
  }

  signOut() {
    this.cookieService.delete(this._tokenKey);
    localStorage.removeItem(this._userNameKey);
  }

  getToken() {
    return this.cookieService.get(this._tokenKey);
  }

  checkUserIsLoggedIn() {
    var token = (this.getToken() || '').trim();

    return token != '';
  }

  getUserName() {
    return localStorage.getItem(this._userNameKey);
  }
}