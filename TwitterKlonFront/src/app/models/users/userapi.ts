import { Injectable } from '@angular/core';
import { CrudService } from '../../services/backend/crud.service'
import { TokenAPI } from '../tokens/tokenapi';
import { User } from './dtos/user';
import { UserEditor } from './dtos/usereditor';
import { UserLogin } from './dtos/userlogin';

@Injectable({
    providedIn: 'root'
})
export class UserAPI {
    public user: User = {firstName: "", lastName: "", address: "", username: "", password: "", id: ""};

    constructor(private service: CrudService, private tokens: TokenAPI) {}

    async getAllUsers(): Promise<User[]> {
        /* return await this.service.getAllUsers(); */
        return await this.service.sendGetRequest("/users");
    }

    async getUser(id: string): Promise<User> {
        /* return await this.service.getUser(id); */
        return await this.service.sendGetRequest("/users/" + id);
    }

    async getUserByUsername(username: string): Promise<User> {
        /* return await this.service.getUserByUsername(username); */
        return await this.service.sendGetRequest("/users/byusername/" + username);
    }

    async register(editor: UserEditor): Promise<User> {
        /* const data = await this.service.register(editor);
        const session = data.session;
        const user = data.user as User;
        this.service.setSessionKey(session.sessionKey);
        this.sessions.activeSession = session;
        this.user = user;
        return user; */
        await this.service.sendPostRequest("/register", JSON.stringify(editor), false);
        await this.login({username: editor.username, password: editor.password});
        this.user = await this.getUserByUsername(editor.username);
        return this.user;
    }

    async login(login: UserLogin): Promise<boolean> {
        /* const session = await this.service.login(login);
        if (session == null) return false;
        this.service.setSessionKey(session.sessionKey);
        this.sessions.activeSession = session;
        this.user = await this.getUser(session.userId);
        localStorage.setItem("session", JSON.stringify(session));
        return true; */
        const success = await this.service.sendPostRequest<boolean>("/login", JSON.stringify(login), false, true);
        if (success) {
            const token = await this.tokens.requestToken();
            console.log(token);
            this.service.setAccessToken(token.sessionKey);
            this.user = await this.getUserByUsername(login.username);
        }
        return success;
    }

    async logout() {
        /* if (this.sessions.activeSession == null) return;
        this.user = {firstName: "", lastName: "", address: "", username: "", password: "", id: ""};
        localStorage.removeItem("session");
        return await this.service.logout(); */
        await this.service.sendDeleteRequest("/logout", true, true);
    }

    async updateUser(id: string, editor: UserEditor): Promise<User> {
        /* const user = await this.service.editUser(id, editor);
        if (user != null) this.user = user;
        return user; */
        const user: User = await this.service.sendPutRequest("/users/" + id, JSON.stringify(editor));
        this.reloadUser();
        return user;
    }

    async deleteUser(id: string) {
        /* this.user = {firstName: "", lastName: "", address: "", username: "", password: "", id: ""};
        localStorage.removeItem("session");
        return await this.service.deleteUser(id); */
        await this.service.sendDeleteRequest("/users/" + id, true, true);
    }

    async isLoggedIn(): Promise<boolean> {
        /* if (this.user?.id == null || this.user.id == "") return false;
        return await this.service.validate(); */
        return this.tokens.validate();
    }

    async reloadUser() {
        this.user = await this.service.sendGetRequest("/ownuser", true, true);
    }
}