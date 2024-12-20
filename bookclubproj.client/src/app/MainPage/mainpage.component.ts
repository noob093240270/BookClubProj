import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css'],
  standalone: false
})

export class MainPageComponent {

  constructor(private router: Router) {
  }

  goToLibrary() { this.router.navigate(['/library']); }
  goToUserPage() { this.router.navigate(['/userpage']); }
}
