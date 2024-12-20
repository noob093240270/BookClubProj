import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserbooklistService {

  private apiUrl = 'http://localhost:5011/api/readbook/userpage';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/userbooks`);
  }

  deleteUserbook(bookId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({'Authorization': `Bearer ${token}`})  : undefined;
    return this.http.post(`${this.apiUrl}/delete`, {bookId}, {headers});

  }

}
