import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {BookListComponent} from './BookList/booklist.component';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'library',
  templateUrl: 'library.component.html',
  imports: [BookListComponent]
})

export class LibraryComponent implements OnInit {

  constructor( private router: Router) {
  }

  ngOnInit() {
  }

}
