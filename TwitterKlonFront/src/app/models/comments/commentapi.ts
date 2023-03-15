import { Injectable } from "@angular/core";
import { Comment } from "./dtos/comment";
import { CrudService } from "../../services/backend/crud.service";
import { CommentEditor } from "./dtos/commenteditor";

@Injectable({
    providedIn: 'root'
})
export class CommentAPI {
    constructor(private service: CrudService) {}

    async getCommentsOnPost(postId: string): Promise<Comment[]> {
        /* return await this.service.getCommentsOnPost(postId); */
        return await this.service.sendGetRequest("/comments/posts/" + postId);
    }

    async addComment(editor: CommentEditor): Promise<Comment> {
        /* return await this.service.addComment(editor); */
        return await this.service.sendPostRequest("/comments", JSON.stringify(editor));
    }

    async editComment(id: string, editor: CommentEditor) {
        /* return await this.service.editComment(id, editor); */
        await this.service.sendPutRequest("/comments/" + id, JSON.stringify(editor));
    }

    async deleteComment(id: string) {
        /* return await this.service.deleteComment(id); */
        await this.service.sendDeleteRequest("/comments/" + id);
    }
}