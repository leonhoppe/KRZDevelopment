import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryAPI } from 'src/app/models/categories/categoryapi';
import { Category } from 'src/app/models/categories/dtos/category';
import { Comment } from 'src/app/models/comments/dtos/comment';
import { CommentAPI } from 'src/app/models/comments/commentapi';
import { CommentEditor } from 'src/app/models/comments/dtos/commenteditor';
import { Post } from 'src/app/models/posts/dtos/post';
import { PostEditor } from 'src/app/models/posts/dtos/posteditor';
import { PostAPI } from 'src/app/models/posts/postapi';
import { User } from 'src/app/models/users/dtos/user';
import { UserAPI } from 'src/app/models/users/userapi';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  constructor(public userApi: UserAPI, private postApi: PostAPI, private categoryApi: CategoryAPI, private commentApi: CommentAPI, private router: Router) { }

  posts: {sender: User, title: string, message: string, category: Category, id: string}[] = [];
  userPosts: {sender: User, title: string, message: string, category: Category, id: string}[] = [];
  categories: Category[] = [];
  currentPost: Post = {id: "", senderId: "", title: "", message: "", categoryId: ""};
  currentPostComments: Comment[] = [];

  getCategoryIndex(id: string): number {
    for (let i = 0; i < this.categories.length; i++) {
      const category = this.categories[i];
      if (category.id == id) return i;
    }
    return 0;
  }

  async ngOnInit() {
    this.openAllPosts();
  }

  async reload() {
    const allPosts = await this.postApi.getAllPosts();
    this.posts = [];
    this.userPosts = [];

    for (let i = 0; i < allPosts.length; i++) {
      const post = allPosts[i];
      const sender = await this.userApi.getUser(post.senderId);
      const category = await this.categoryApi.getCategory(post.categoryId);
      this.posts.push({sender: sender, title: post.title, message: post.message, category: category, id: post.id});
    }

    this.categories = await this.categoryApi.getAllCategories();

    if (this.currentPost.id != "") {
      this.currentPostComments = await this.commentApi.getCommentsOnPost(this.currentPost.id);
      for (let comment of this.currentPostComments) {
        comment.senderId = await this.getUsername(comment.senderId);
      }
    }

    if (await this.userApi.isLoggedIn()) {
      const user = this.userApi.user;
      this.posts.forEach(post => { if (post.sender.id == user.id) this.userPosts.push(post) });
    }

    this.clearInputs();
  }

  showObject(section: HTMLElement | null) {
    if (section == null) return;
    const sections = document.getElementsByClassName("sections") as HTMLCollectionOf<HTMLElement>;
    for (let i = 0; i < sections.length; i++) {
      const s = sections[i] as HTMLElement;
      s.style.display = "none";
    }
    section.style.display = "block";
  }

  async openAllPosts() {
    await this.reload();
    this.showObject(document.getElementById("allPosts"));
  }

  async openUserPosts() {
    if (!await this.userApi.isLoggedIn()) {
      this.router.navigate(["/users"], {queryParams: {openLogin: true}});
    }else {
      await this.reload();
      this.showObject(document.getElementById("userPosts"));
    }
  }

  async openCreatePost() {
    if (!await this.userApi.isLoggedIn()) {
      this.router.navigate(["/users"], {queryParams: {openLogin: true}});
    }else {
      this.showObject(document.getElementById("createPost"));
    }
  }

  async createPost() {
    const title = document.getElementById("create_title") as HTMLInputElement;
    const message = document.getElementById("create_message") as HTMLInputElement;
    const categoryId = document.getElementById("create_category") as HTMLSelectElement;
    const editor: PostEditor = {senderId: this.userApi.user.id, title: title.value, message: message.value, categoryId: this.categories[categoryId.selectedIndex].id};
    await this.postApi.addPost(editor);
    Swal.fire({
      icon: 'success',
      title: 'Post erstellt',
      showConfirmButton: false,
      timer: 1500
    });
    this.openUserPosts();
  }

  async deletePost(postId: string) {
    await this.postApi.deletePost(postId);
    Swal.fire({
      icon: 'success',
      title: 'Post gelöscht',
      showConfirmButton: false,
      timer: 1500
    });
    await this.reload();
  }

  showEditPost(post: {sender: User, title: string, message: string, category: Category, id: string}) {
    this.currentPost = {id: post.id, senderId: post.sender.id, title: post.title, message: post.message, categoryId: post.category.id};
    (document.getElementById("edit_category") as HTMLSelectElement).selectedIndex = this.getCategoryIndex(post.category.id);
    this.showObject(document.getElementById("editPost"));
  }

  async editPost() {
    const title = document.getElementById("edit_title") as HTMLInputElement;
    const message = document.getElementById("edit_message") as HTMLTextAreaElement;
    const category = document.getElementById("edit_category") as HTMLSelectElement;
    const editor: PostEditor = {
      title: title.value != "" ? title.value : this.currentPost.title,
      message: message.value != "" ? message.value : this.currentPost.message,
      categoryId: this.categories[category.selectedIndex].id,
      senderId: this.currentPost.senderId
    };
    await this.postApi.editPost(this.currentPost.id, editor);
    Swal.fire({
      icon: 'success',
      title: 'Post bearbeitet',
      showConfirmButton: false,
      timer: 1500
    });
    await this.openUserPosts();
  }

  async getCommentsOnPost(postId: string) {
    this.currentPost = await this.postApi.getPost(postId);
    await this.reload();
    this.showObject(document.getElementById("comments"));
  }

  async getUsername(userId: string) {
    return (await this.userApi.getUser(userId)).username;
  }

  async deleteComment(id: string) {
    await this.commentApi.deleteComment(id);
    Swal.fire({
      icon: 'success',
      title: 'Kommentar gelöscht',
      showConfirmButton: false,
      timer: 1500
    });
    await this.reload();
  }

  async showAddComment(postId: string) {
    this.currentPost = await this.postApi.getPost(postId);
    this.showObject(document.getElementById("addComment"));
  }

  async addComment() {
    const text = document.getElementById("comment_text") as HTMLTextAreaElement;
    const editor: CommentEditor = {senderId: this.userApi.user.id, postId: this.currentPost.id, text: text.value};
    await this.commentApi.addComment(editor);
    Swal.fire({
      icon: 'success',
      title: 'Kommentar hinzugefügt',
      showConfirmButton: false,
      timer: 1500
    });
    await this.getCommentsOnPost(this.currentPost.id);
  }

  clearInputs() {
    const inputs = document.getElementsByClassName("clearOnSubmit");
    for (let i = 0; i < inputs.length; i++) {
      const input = inputs[i] as HTMLInputElement;
      input.value = "";
    }
  }

}
