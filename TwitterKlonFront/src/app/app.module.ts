import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CategoryAPI } from './models/categories/categoryapi';
import { CommentAPI } from './models/comments/commentapi';
import { PostAPI } from './models/posts/postapi';
import { TokenAPI } from './models/tokens/tokenapi';
import { UserAPI } from './models/users/userapi';
import { CategoriesComponent } from './pages/categories/categories.component';
import { PostsComponent } from './pages/posts/posts.component';
import { UsersComponent } from './pages/users/users.component';

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    PostsComponent,
    CategoriesComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    TokenAPI,
    CategoryAPI,
    CommentAPI,
    PostAPI,
    UserAPI
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
