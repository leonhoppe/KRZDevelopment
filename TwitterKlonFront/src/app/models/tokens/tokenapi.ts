import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { CrudService } from "../../services/backend/crud.service";
import { AccessToken } from "./dtos/accesstoken";

@Injectable({
    providedIn: 'root'
})
export class TokenAPI {
    constructor(private service: CrudService, private router: Router) {}

    async restoreToken() {
        try {
            const token = await this.requestToken();
            this.service.setAccessToken(token.sessionKey);
        }catch {
            this.router.navigate(["/users"], {queryParams: {openLogin: true}});
        }
    }

    async validate(): Promise<boolean> {
        return await this.service.sendGetRequest("/validate");
    }

    async requestToken(): Promise<AccessToken> {
        return await this.service.sendGetRequest("/token", false, true);
    }
}