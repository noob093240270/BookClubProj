import {Component, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {Token} from '@angular/compiler';




@Injectable({
  providedIn: 'root'
})
export class BookService {

  private apiUrl = 'http://localhost:5011/api/book';

  constructor(private http: HttpClient) { }

  getBook(): Observable<any> {
    return this.http.get(`${this.apiUrl}/library/books`);
  }

  addToReadBooks(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({'Authorization': `Bearer ${token}`})  : undefined;
    return this.http.post(`${this.apiUrl}/add`, {bookId}, {headers});

  }




}
