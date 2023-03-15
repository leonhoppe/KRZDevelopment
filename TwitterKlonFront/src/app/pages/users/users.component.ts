import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/users/dtos/user';
import { UserEditor } from 'src/app/models/users/dtos/usereditor';
import { UserLogin } from 'src/app/models/users/dtos/userlogin';
import { UserAPI } from 'src/app/models/users/userapi';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  constructor(public userApi: UserAPI, private activatedRoute: ActivatedRoute) {}

  users: User[]

  async ngOnInit() {
    await this.reload();

    if (this.activatedRoute.snapshot.queryParams["openLogin"]) {
      this.showObject(document.getElementById("login"));
    }else { 
      this.showObject(document.getElementById("allUsers"));
    }
  }

  async reload() {
    this.users = await this.userApi.getAllUsers();
  }

  async login() {
    const username = document.getElementById("login_username") as HTMLInputElement;
    const password = document.getElementById("login_password") as HTMLInputElement;
    const login: UserLogin = {username: username.value, password: password.value};
    const success = await this.userApi.login(login);
    if (success) {
      Swal.fire({
        icon: 'success',
        title: 'Erfolgreich angemeldet',
        showConfirmButton: false,
        timer: 1500
      });
      this.clearInputs();
      this.showEditUser();
    }else {
      Swal.fire({
        icon: 'error',
        title: 'Fehler',
        text: 'Benutzername oder Passwort ist falsch!',
      });
    }
  }

  async logout() {
    if (await this.userApi.isLoggedIn()) {
      await this.userApi.logout();
      Swal.fire({
        icon: 'success',
        title: 'Erfolgreich abgemeldet',
        showConfirmButton: false,
        timer: 1500
      });
      this.showAll();
    }else {
      Swal.fire({
        icon: 'error',
        title: 'Fehler',
        text: 'Du bist nicht eingeloggt!',
        timer: 1500,
        showConfirmButton: false
      });
    }
  }

  async register() {
    const firstname = document.getElementById("register_firstname") as HTMLInputElement;
    const lastname = document.getElementById("register_lastname") as HTMLInputElement;
    const username = document.getElementById("register_username") as HTMLInputElement;
    const password = document.getElementById("register_password") as HTMLInputElement;
    const address = document.getElementById("register_address") as HTMLInputElement;
    const editor: UserEditor = {username: username.value, password: password.value, firstName: firstname.value, lastName: lastname.value, address: address.value};
    const user = await this.userApi.register(editor);
    Swal.fire({
      icon: 'success',
      title: 'Du bist nun als ' + user.username + ' angemeldet',
      showConfirmButton: false,
      timer: 1500
    });
    this.clearInputs();
    this.showEditUser();
  }

  async deleteUser() {
    if (await this.userApi.isLoggedIn()) {
      await this.userApi.deleteUser(this.userApi.user.id);
      Swal.fire({
        icon: 'success',
        title: 'Account gelöscht',
        showConfirmButton: false,
        timer: 1500
      });
      this.showAll();
    }else {
      Swal.fire({
        icon: 'error',
        title: 'Fehler',
        text: 'Du bist nicht eingeloggt!',
        timer: 1500,
        showConfirmButton: false
      });
    }
  }

  async editUser() {
    const firstname = document.getElementById("edituser_firstname") as HTMLInputElement;
    const lastname = document.getElementById("edituser_lastname") as HTMLInputElement;
    const username = document.getElementById("edituser_username") as HTMLInputElement;
    const password = document.getElementById("edituser_password") as HTMLInputElement;
    const address = document.getElementById("edituser_address") as HTMLInputElement;
    const editor: UserEditor = {username: username.value, password: password.value, firstName: firstname.value, lastName: lastname.value, address: address.value};

    if (editor.username == "") editor.username = this.userApi.user.username;
    if (editor.password == "") editor.password = this.userApi.user.password;
    if (editor.firstName == "") editor.firstName = this.userApi.user.firstName;
    if (editor.lastName == "") editor.lastName = this.userApi.user.lastName;
    if (editor.address == "") editor.address = this.userApi.user.address;

    await this.userApi.updateUser(this.userApi.user.id, editor);
    Swal.fire({
      icon: 'success',
      title: 'Änderungen gespeichert',
      showConfirmButton: false,
      timer: 1500
    });

    this.clearInputs();
  }

  clearInputs() {
    const inputs = document.getElementsByClassName("clearOnSubmit");
    for (let i = 0; i < inputs.length; i++) {
      const input = inputs[i] as HTMLInputElement;
      input.value = "";
    }
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

  async showAll() {
    await this.reload();
    this.clearInputs();
    const section = document.getElementById("allUsers");
    this.showObject(section);
  }

  showLogin() {
    const section = document.getElementById("login");
    this.showObject(section);
  }

  showRegister() {
    const section = document.getElementById("register");
    this.showObject(section);
  }

  async showEditUser() {
    if (await this.userApi.isLoggedIn()) {
      const section = document.getElementById("edituser");
      this.showObject(section);
    }else {
      Swal.fire({
        icon: 'error',
        title: 'Fehler',
        text: 'Du bist nicht eingeloggt!',
        timer: 1500,
        showConfirmButton: false
      });
    }
  }
}
