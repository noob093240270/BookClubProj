import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {UserbooklistService} from './UserBookList/userbooklist.service';
import {UserBookListComponent} from './UserBookList/userbooklist.component';
import { AuthService } from '../auth.service';
import {NgIf} from '@angular/common';
import {UserAddedBooklistComponent} from './UserAddedBookList/userAddedBooklist.component';

@Component({
  selector: 'user-page',
  templateUrl: 'userpage.component.html',
  styleUrls: ['userpage.component.css'],
  imports: [UserBookListComponent, NgIf, UserAddedBooklistComponent]
})

export class UserPageComponent {
  username: string | null = null;

  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    this.authService.getUserName().subscribe({
      next: user => {
        this.username = user.name;
      },
      error: err => {
        console.log("не удалось получить имя пользвоателя");
      }
    })
  }

}
