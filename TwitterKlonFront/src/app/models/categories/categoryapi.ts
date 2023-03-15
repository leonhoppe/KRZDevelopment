import { Injectable } from "@angular/core";
import { CrudService } from "../../services/backend/crud.service";
import { Category } from "./dtos/category";
import { CategoryEditor } from "./dtos/categoryeditor";

@Injectable({
    providedIn: 'root'
})
export class CategoryAPI {
    constructor(private service: CrudService) {}

    async getAllCategories(): Promise<Category[]> {
        /* return await this.service.getAllCategories(); */
        return await this.service.sendGetRequest("/categories");
    }

    async getCategory(id: string): Promise<Category> {
        /* return await this.service.getCategory(id); */
        return await this.service.sendGetRequest("/categories/" + id);
    }

    async addCategory(editor: CategoryEditor): Promise<Category> {
        /* return await this.service.addCategory(editor); */
        return await this.service.sendPostRequest("/categories", JSON.stringify(editor));
    }

    async editCategory(id: string, editor: CategoryEditor): Promise<Category> {
        /* return await this.service.editCategory(id, editor); */
        return await this.service.sendPutRequest("/categories/" + id, JSON.stringify(editor));
    }

    async deleteCategory(id: string) {
        /* return await this.service.deleteCategory(id); */
        await this.service.sendDeleteRequest("/categories/" + id);
    }
}