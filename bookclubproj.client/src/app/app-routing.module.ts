import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Login/login.component';
import { RegisterComponent } from './Register/register.component';
import {BookListComponent} from './Library/BookList/booklist.component';
import {UserbooklistService} from './UserPage/UserBookList/userbooklist.service';
import {MainPageComponent} from './MainPage/mainpage.component';
import {LibraryComponent} from './Library/library.component';
import {UserPageComponent} from './UserPage/userpage.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'books', component: BookListComponent },
  { path: 'readbook', component: UserbooklistService},
  { path: 'mainpage', component: MainPageComponent},
  { path: 'library', component: LibraryComponent},
  { path: 'userpage', component: UserPageComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
