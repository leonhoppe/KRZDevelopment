import { Injectable } from "@angular/core";
import { CrudService } from "../../services/backend/crud.service";
import { Post } from "./dtos/post";
import { PostEditor } from "./dtos/posteditor";

@Injectable({
    providedIn: 'root'
})
export class PostAPI {
    constructor(private service: CrudService) {}

    async getAllPosts(): Promise<Post[]> {
        /* return await this.service.getAllPosts(); */
        return await this.service.sendGetRequest("/posts");
    }

    async getPost(id: string): Promise<Post> {
        /* return await this.service.getPost(id); */
        return await this.service.sendGetRequest("/posts/" + id);
    }

    async addPost(editor: PostEditor): Promise<Post> {
        /* return await this.service.addPost(editor); */
        return await this.service.sendPostRequest("/posts", JSON.stringify(editor));
    }

    async editPost(id: string, editor: PostEditor): Promise<Post> {
        /* return await this.service.editPost(id, editor); */
        return await this.service.sendPutRequest("/posts/" + id, JSON.stringify(editor));
    }

    async deletePost(id: string): Promise<void> {
        /* return await this.service.deletePost(id); */
        return await this.service.sendDeleteRequest("/posts/" + id);
    }
}