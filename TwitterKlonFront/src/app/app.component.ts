import { Component, OnInit } from '@angular/core';
import { TokenAPI } from './models/tokens/tokenapi';
import { UserAPI } from './models/users/userapi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  ready: boolean = false;

  constructor(private tokenApi: TokenAPI, private userApi: UserAPI) { }

  async ngOnInit() {
    await this.tokenApi.restoreToken();
    if (await this.userApi.isLoggedIn()) this.userApi.reloadUser();
    this.ready = true;
  }
}
