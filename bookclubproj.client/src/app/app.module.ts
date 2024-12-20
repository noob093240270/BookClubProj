import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './Login/login.component';
import { RegisterComponent } from './Register/register.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {AuthInterceptor} from './Login/auth.interceptor';
import {HeaderComponent} from './MainPage/Header/header.component';
import {UserBookListComponent} from './UserPage/UserBookList/userbooklist.component';
import {MainPageComponent} from './MainPage/mainpage.component';
import {UserPageComponent} from './UserPage/userpage.component';
import  {LibraryComponent} from './Library/library.component';
import {BookListComponent} from './Library/BookList/booklist.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainPageComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    HeaderComponent,
    UserBookListComponent,
    BookListComponent,
    LibraryComponent,
    UserPageComponent,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
