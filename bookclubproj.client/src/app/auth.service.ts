import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loggedIn =new BehaviorSubject<boolean>(this.checkLoginStatus());
  isLoggedIn = this.loggedIn.asObservable();

  constructor(private http: HttpClient){}

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

  getUserName(): Observable<{ name: string }> {
    return this.http.get<{ name: string }>('http://localhost:5011/api/user/name');
  }


}
