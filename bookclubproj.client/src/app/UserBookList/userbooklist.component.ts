import { Component, OnInit } from '@angular/core';
import { BookService } from './userbooklist.service';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'userbooklist',
  templateUrl: 'userbooklist.component.html',

  imports: [
    NgForOf
  ]
})

export class UserBookListComponent implements OnInit {
  books: any[] = [];
  userId: number = 1;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
  }

  loadBooks(): void {
    this.bookService.getBooks(this.userId).subscribe(data => {
      this.books = data;
    });
  }
}
