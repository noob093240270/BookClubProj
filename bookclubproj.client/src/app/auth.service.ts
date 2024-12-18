import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loggedIn =new BehaviorSubject<boolean>(this.checkLoginStatus());
  isLoggedIn = this.loggedIn.asObservable();

  constructor(){}

  private checkLoginStatus(): boolean {
    const token = localStorage.getItem('token');

    return !!token;
  }

  login(token: string) {
    localStorage.setItem('token', token);
    this.loggedIn.next(true);
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }


}
