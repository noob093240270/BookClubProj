import { Component, OnInit } from '@angular/core';
import {UserbooklistService} from './userbooklist.service';
import {NgForOf, NgIf} from '@angular/common';
import {Router} from '@angular/router';
import {AuthService} from '../../auth.service';

@Component({
  selector: 'userbooklist',
  templateUrl: 'userbooklist.component.html',

  imports: [
    NgForOf,
    NgIf
  ]
})

export class UserBookListComponent implements OnInit {
  books: any[] = [];
  isLogged : boolean = false;

  constructor(private userbooklistService: UserbooklistService, private router: Router, protected authService: AuthService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe((isLoggedIn: boolean) => {
      this.isLogged = isLoggedIn;
    })
    if (this.isLogged)
    {
      this.loadBooks();
    }
  }

  loadBooks(): void {
    this.userbooklistService.getBooks().subscribe(
      (response) => {
        this.books = response;
        console.log(this.books);
      },
      (error) => {
        console.error('Ошибка при получении данных', error);
      }
    );
  }

  deleteBookFromReadList(bookId: number): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.userbooklistService.deleteUserbook(bookId).subscribe(
        () => {
          alert("книга удалена из вашей коллекции");
        },
        (error) => {
          console.log(error);
          alert("ошибка удаления");
        }
      );
    }
    else
    {
      this.router.navigate(['/login']);
    }
  }
}
