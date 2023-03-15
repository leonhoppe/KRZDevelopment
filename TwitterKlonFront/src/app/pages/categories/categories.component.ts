import { Component, OnInit } from '@angular/core';
import { CategoryAPI } from 'src/app/models/categories/categoryapi';
import { Category } from 'src/app/models/categories/dtos/category';
import { CategoryEditor } from 'src/app/models/categories/dtos/categoryeditor';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  constructor(private categoryApi: CategoryAPI) {}

  categories: Category[];
  currentCategory: Category = {id: "", name: "", tags: ""};

  ngOnInit(): void {
    this.showAllCategories();
  }

  async reload() {
    this.categories = await this.categoryApi.getAllCategories();
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

  async showAllCategories() {
    await this.reload();
    this.showObject(document.getElementById("allCategories"));
  }

  showAddCategory() {
    this.showObject(document.getElementById("addCategory"));
  }

  async showEditCategory(id: string) {
    this.currentCategory = await this.categoryApi.getCategory(id);
    this.showObject(document.getElementById("editCategory"));
  }

  async addCategory() {
    const name = document.getElementById("add_name") as HTMLInputElement;
    const tags = document.getElementById("add_tags") as HTMLInputElement;
    const editor: CategoryEditor = {name: name.value, tags: tags.value};
    await this.categoryApi.addCategory(editor);
    Swal.fire({
      icon: 'success',
      title: 'Kategorie erstellt',
      showConfirmButton: false,
      timer: 1500
    });
    await this.showAllCategories();
  }

  async deleteCategory(id: string) {
    await this.categoryApi.deleteCategory(id);
    await this.reload();
    Swal.fire({
      icon: 'success',
      title: 'Kategorie gelöscht',
      showConfirmButton: false,
      timer: 1500
    });
  }

  async editCategory() {
    const name = document.getElementById("edit_name") as HTMLInputElement;
    const tags = document.getElementById("edit_tags") as HTMLInputElement;
    const editor: CategoryEditor = {
      name: name.value != "" ? name.value : this.currentCategory.name,
      tags: tags.value != "" ? tags.value : this.currentCategory.tags
    };
    await this.categoryApi.editCategory(this.currentCategory.id, editor);
    Swal.fire({
      icon: 'success',
      title: 'Kategorie bearbeitet',
      showConfirmButton: false,
      timer: 1500
    });
    await this.showAllCategories();
  }

  clearInputs() {
    const inputs = document.getElementsByClassName("clearOnSubmit");
    for (let i = 0; i < inputs.length; i++) {
      const input = inputs[i] as HTMLInputElement;
      input.value = "";
    }
  }

}
