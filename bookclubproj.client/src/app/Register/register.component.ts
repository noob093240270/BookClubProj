import { Component } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
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
        alert('Регистрация прошла успешно, войдите в систему');
        this.router.navigate(['/login']);
      },
      (error: HttpErrorResponse) => {
        console.log(error);
        alert(`Ошибка регистрации: ${error.error.name[0]}`);
      }
    )
  }
}
