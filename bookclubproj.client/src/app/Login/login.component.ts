import {Component, Injectable, OnInit} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import { RouterModule } from '@angular/router';
import {CommonModule, NgIf} from '@angular/common';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {AuthService} from '../auth.service';




@Component({
  selector: "app-login",
  templateUrl: './login.component.html',
  standalone: false

})


export class LoginComponent {
  name: string = '';
  password: string = '';
  errorMessage: string = '';
  private apiUrl = 'http://localhost:5011/api/auth/login';


  constructor(private http: HttpClient, private router: Router,private authService: AuthService) {}

  onLogin(): void {
    const loginData = { name: this.name, password: this.password };

    this.login(loginData.name, loginData.password).subscribe(
      {
        next: (res: any) => {
          console.log('вы успешно вошли',res);
          if (res.token) {
            localStorage.setItem('token', res.token);
            this.authService.login(res.token);
          }
          this.router.navigate(['/books']);
        },
        error: (err) => {
          console.error('ошибка',err);
          this.errorMessage = 'неверный логин или пароль';
        }
      }
    );
  }

  login(name: string, password: string) {
    const body = { Name: name, Password : password };
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',

    });

    return this.http.post(this.apiUrl, body, { headers });
  }

}



