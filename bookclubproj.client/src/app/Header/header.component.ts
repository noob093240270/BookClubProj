import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {NgIf} from '@angular/common';
import {AuthService} from '../auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  imports: [
    NgIf
  ],
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  isLogged : boolean = false;

  constructor(private router: Router, protected authService: AuthService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe((isLoggedIn: boolean) => {
      this.isLogged = isLoggedIn;
    })
  }


  logout(){
    this.authService.logout();
    this.router.navigate(['login']);
  }



}
