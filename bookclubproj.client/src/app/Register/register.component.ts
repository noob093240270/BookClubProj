import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Router} from '@angular/router';


@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: 'register.component.html'
})

export class RegisterComponent  {
  name: string = '';
  password: string = '';
  errormessage: string = '';
  successmessage: string = '';

  constructor(private http: HttpClient,private router: Router) { }

  onRegister(){
    const registerData = {name: this.name, password: this.password};
    this.http.post('http://localhost:5011/api/auth/register', registerData).subscribe(
      (response) => {
        console.log('Registration successful:', response);
      },
      (error) => {
        console.error('Registration failed:', error);
        // Выводим подробности ошибки
        if (error.status) {
          console.log('Error status:', error.status);  // Статус ошибки HTTP
        }
        if (error.error) {
          console.log('Error message:', error.error);  // Сообщение об ошибке
        }
      }
    )
  }
}
