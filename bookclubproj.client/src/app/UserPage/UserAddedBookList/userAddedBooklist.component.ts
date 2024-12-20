import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserbooklistService} from '../UserBookList/userbooklist.service';
import {Router} from '@angular/router';
import {AuthService} from '../../auth.service';
import {UserAddedBookListService} from './userAddedBooklist.service';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';


@Component({
  selector: 'user-added-booklist',
  templateUrl: './userAddedBooklist.component.html',
  imports: [
    NgForOf,
    NgIf,
    FormsModule
  ],
  styleUrls: ['./userAddedBooklist.component.css']
})

export class UserAddedBooklistComponent implements OnInit {

  books: any[] = [];
  isLogged : boolean = false;
  title = '';
  author = '';

  constructor(private useraddedBookservice: UserAddedBookListService, private router: Router, protected authService: AuthService) { }

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
    this.useraddedBookservice.getBooks().subscribe(
      (response) => {
        this.books = response;
        console.log(this.books);
      },
      (error) => {
        console.error('Ошибка при получении данных', error);
      }
    );
  }


  addUsersAddedBook(): void {
    const token = localStorage.getItem('token');
    if (!this.title || !this.author) {
      alert("название и автор - обязательные поля");
      return;
    }

    const newBook = {title: this.title, author: this.author};

    this.useraddedBookservice.setBook(newBook).subscribe(
      {
        next: ()=>{
          alert("Книга успешно добавлена");
          this.title = '';
          this.author = '';
        },
        error: (err) => {
          console.error('Ошибка при добавлении книги:', err);
          alert("Ошибка добавления книги, может она уже существует?");
        }
      }
    )

  }


  deleteUsersAddedBook(bookId: number): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.useraddedBookservice.deleteBook(bookId).subscribe(
        () => {
          alert("книга удалена из ваших добавленных");
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




