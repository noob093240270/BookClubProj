
import {Component, Injectable, OnInit} from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';


@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  standalone: false
})

export class AppComponent implements OnInit {

  constructor(private router: Router) { }

  title = 'Книжный клуб';
  ngOnInit() {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/login']);
    }
  }
}
