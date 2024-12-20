import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class UserAddedBookListService {

  private apiUrl = 'http://localhost:5011/api/usersaddedbook';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/get_users_added_book`);
  }

  setBook(newBook: { title: string; author: string }): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ 'Authorization': `Bearer ${token}` }) : undefined;
    return this.http.post(`${this.apiUrl}/add_users_book`, newBook, {headers});
  }

  deleteBook(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({'Authorization': `Bearer ${token}`})  : undefined;
    return this.http.post(`${this.apiUrl}/delete`, bookId, {headers});

  }

}
