import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { AccessToken } from 'src/app/models/tokens/dtos/accesstoken';

@Injectable({
  providedIn: 'root'
})
export class CrudService {
  private endPoint = 'http://localhost:5000/api';

  constructor(private httpClient: HttpClient, private router: Router) {}

  private httpHeader = new HttpHeaders({
      'Content-Type': 'application/json'
  });

  setAccessToken(key?: string) {
    if (key == null) return;
    this.httpHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': key
    });
  }

  async sendGetRequest<T>(url: string, authorised: boolean = true, withCredentials: boolean = false): Promise<T> {
    try {
      return await firstValueFrom(this.httpClient.get<T>(this.endPoint + url, {headers: this.httpHeader, withCredentials}));
    }catch(e) {
      const httpErrorResponse = e as HttpErrorResponse;
      if (authorised && httpErrorResponse.status == 403) {
        await this.checkToken();
        return await firstValueFrom(this.httpClient.get<T>(this.endPoint + url, {headers: this.httpHeader, withCredentials}));
      }else {
        throw e;
      }
    }
  }

  async sendPostRequest<T>(url: string, body?: string, authorised: boolean = true, withCredentials: boolean = false): Promise<T> {
    try {
      return await firstValueFrom(this.httpClient.post<T>(this.endPoint + url, body, {headers: this.httpHeader, withCredentials}));
    }catch(e) {
      const httpErrorResponse = e as HttpErrorResponse;
      if (authorised && httpErrorResponse.status == 403) {
        await this.checkToken();
        return await firstValueFrom(this.httpClient.post<T>(this.endPoint + url, body, {headers: this.httpHeader, withCredentials}));
      }else {
        throw e;
      }
    }
  }

  async sendPutRequest<T>(url: string, body?: string, authorised: boolean = true, withCredentials: boolean = false): Promise<T> {
    try {
      return await firstValueFrom(this.httpClient.put<T>(this.endPoint + url, body, {headers: this.httpHeader, withCredentials}));
    }catch(e) {
      const httpErrorResponse = e as HttpErrorResponse;
      if (authorised && httpErrorResponse.status == 403) {
        await this.checkToken();
        return await firstValueFrom(this.httpClient.put<T>(this.endPoint + url, body, {headers: this.httpHeader, withCredentials}));
      }else {
        throw e;
      }
    }
  }

  async sendDeleteRequest<T>(url: string, authorised: boolean = true, withCredentials: boolean = false): Promise<T> {
    try {
      return await firstValueFrom(this.httpClient.delete<T>(this.endPoint + url, {headers: this.httpHeader, withCredentials}));
    }catch(e) {
      const httpErrorResponse = e as HttpErrorResponse;
      if (authorised && httpErrorResponse.status == 403) {
        await this.checkToken();
        return await firstValueFrom(this.httpClient.delete<T>(this.endPoint + url, {headers: this.httpHeader, withCredentials}));
      }else {
        throw e;
      }
    }
  }

  async checkToken(): Promise<boolean> {
    let valid = await firstValueFrom(this.httpClient.get<boolean>(this.endPoint + "/validate", {headers: this.httpHeader}));
    if (!valid) {
      try {
        const token = await firstValueFrom(this.httpClient.get<AccessToken>(this.endPoint + "/token", {headers: this.httpHeader, withCredentials: true}));
        this.setAccessToken(token.sessionKey);
        valid = await firstValueFrom(this.httpClient.get<boolean>(this.endPoint + "/validate", {headers: this.httpHeader}));
        if (!valid) {
          this.router.navigate(["/users"], {queryParams: {openLogin: true}});
        }
      }catch {
        this.router.navigate(["/users"], {queryParams: {openLogin: true}});
      }
    }
    return valid;
  }

  /* //--------------------------------------------------Users--------------------------------------------------

  async getAllUsers(): Promise<User[]> {
    return await firstValueFrom(this.httpClient.get<User[]>(this.endPoint + '/users', {headers: this.httpHeader}));
  }

  async getUser(id: string): Promise<User> {
    return await firstValueFrom(this.httpClient.get<User>(this.endPoint + "/users/" + id, {headers: this.httpHeader}));
  }

  async getUserByUsername(username: string): Promise<User> {
    return await firstValueFrom(this.httpClient.get<User>(this.endPoint + "/users/byusername/" + username, {headers: this.httpHeader}));
  }

  async addUser(editor: UserEditor): Promise<User> {
    return await firstValueFrom(this.httpClient.post<User>(this.endPoint + "/users", JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async editUser(id: string, editor: UserEditor): Promise<User> {
    return await firstValueFrom(this.httpClient.put<User>(this.endPoint + "/users/" + id, JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async deleteUser(id: string): Promise<void> {
    return await firstValueFrom(this.httpClient.delete<void>(this.endPoint + "/users/" + id, {headers: this.httpHeader}));
  }

  //--------------------------------------------------Sessions--------------------------------------------------

  async login(login: UserLogin): Promise<Session> {
    return await firstValueFrom(this.httpClient.post<Session>(this.endPoint + "/login", JSON.stringify(login), {headers: this.httpHeader}));
  }

  async logout(): Promise<void> {
    return await firstValueFrom(this.httpClient.post<void>(this.endPoint + "/logout", "", {headers: this.httpHeader}));
  }

  async validate(): Promise<boolean> {
    try {
      await firstValueFrom(this.httpClient.post<void>(this.endPoint + "/validate", "", {headers: this.httpHeader}));
    } catch {
      return false;
    }
    return true;
  }

  async register(editor: UserEditor): Promise<{user: User, session: Session}> {
    const result = await firstValueFrom(this.httpClient.post<any[]>(this.endPoint + "/register", JSON.stringify(editor), {headers: this.httpHeader}));
    const user = result[0] as User;
    const session = result[1] as Session;

    return { user, session };
  }

  //--------------------------------------------------Posts--------------------------------------------------

  async getAllPosts(): Promise<Post[]> {
    return await firstValueFrom(this.httpClient.get<Post[]>(this.endPoint + "/posts", {headers: this.httpHeader}));
  }

  async getPost(id: string): Promise<Post> {
    return await firstValueFrom(this.httpClient.get<Post>(this.endPoint + "/posts/" + id, {headers: this.httpHeader}));
  }

  async addPost(editor: PostEditor): Promise<Post> {
    return await firstValueFrom(this.httpClient.post<Post>(this.endPoint + "/posts", JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async editPost(id: string, editor: PostEditor): Promise<Post> {
    return await firstValueFrom(this.httpClient.put<Post>(this.endPoint + "/posts/" + id, JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async deletePost(id: string): Promise<void> {
    return await firstValueFrom(this.httpClient.delete<void>(this.endPoint + "/posts/" + id, {headers: this.httpHeader}));
  }

  //--------------------------------------------------Comments--------------------------------------------------

  async getCommentsOnPost(postId: string): Promise<Comment[]> {
    return await firstValueFrom(this.httpClient.get<Comment[]>(this.endPoint + "/comments/posts/" + postId, {headers: this.httpHeader}));
  }

  async addComment(editor: CommentEditor): Promise<Comment> {
    return await firstValueFrom(this.httpClient.post<Comment>(this.endPoint + "/comments", JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async editComment(id: string, editor: CommentEditor): Promise<Comment> {
    return await firstValueFrom(this.httpClient.put<Comment>(this.endPoint + "/comments/" + id, JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async deleteComment(id: string): Promise<void> {
    return await firstValueFrom(this.httpClient.delete<void>(this.endPoint + "/comments/" + id, {headers: this.httpHeader}));
  }

  //--------------------------------------------------Categories--------------------------------------------------

  async getAllCategories(): Promise<Category[]> {
    return await firstValueFrom(this.httpClient.get<Category[]>(this.endPoint + "/categories", {headers: this.httpHeader}));
  }

  async getCategory(id: string): Promise<Category> {
    return await firstValueFrom(this.httpClient.get<Category>(this.endPoint + "/categories/" + id, {headers: this.httpHeader}));
  }

  async addCategory(editor: CategoryEditor): Promise<Category> {
    return await firstValueFrom(this.httpClient.post<Category>(this.endPoint + "/categories", JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async editCategory(id: string, editor: CategoryEditor): Promise<Category> {
    return await firstValueFrom(this.httpClient.put<Category>(this.endPoint + "/categories/" + id, JSON.stringify(editor), {headers: this.httpHeader}));
  }

  async deleteCategory(id: string): Promise<void> {
    return await firstValueFrom(this.httpClient.delete<void>(this.endPoint + "/categories/" + id, {headers: this.httpHeader}));
  } */
}