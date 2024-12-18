import { Component, OnInit } from '@angular/core';
import {BookService} from './book.service';
import {NgForOf, NgIf} from '@angular/common';
import {AuthService} from '../auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-book-list',
  templateUrl: './booklist.component.html',
  imports: [
    NgIf,
    NgForOf
  ],
  styleUrls: ['./booklist.component.css']
})


export class BookListComponent implements OnInit {
  books: any[] = [];
  error: boolean = false;
  loading: boolean = false;

  constructor(private bookService: BookService,private authService: AuthService,
              private router: Router) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.loading = true;
    this.bookService.getBook().subscribe(
      (response) => {
        this.books = response;
        console.log(this.books);
      },
      (error) => {
        console.error('Ошибка при получении данных', error);
      }
    );
  }

  addBookToReadList(bookId: number): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.bookService.addToReadBooks(bookId).subscribe(
        () => {
          alert("книга добавлена в вашу коллекцию");
        },
        (error) => {
          console.log(error);
          alert("ошибка добавления");
        }
      );
    }
    else{
      this.router.navigate(['/login']);
    }
  }


}
