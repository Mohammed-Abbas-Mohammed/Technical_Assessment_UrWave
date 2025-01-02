import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category/category.service';
import { CommonModule } from '@angular/common';
import { CategoryFormComponent } from '../category-form/category-form.component';




@Component({
  selector: 'app-category',
  imports:[CommonModule  ,CategoryFormComponent],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],

})
export class CategoryComponent implements OnInit {
  categories: any[] = [];
  selectedCategory: Category | null = null; 
  showForm = false; // Toggle form visibility


  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    // this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  createCategory(): void {
    this.selectedCategory = null; 
    this.showForm = true;
  }

  editCategory(category: Category): void {
    this.selectedCategory = { ...category }; // Clone the category
    this.showForm = true;
  }

  deleteCategory(id: string): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.categoryService.deleteCategory(id).subscribe(() => {
        this.loadCategories(); // Reload categories after deletion
      });
    }
  }

  saveCategory(category: Category): void {
    if (category.id) {
      this.categoryService.updateCategory(category.id,category).subscribe(() => {
        this.loadCategories();
        this.showForm = false;
      });
    } else {
      this.categoryService.createCategory(category).subscribe(() => {
        this.loadCategories();
        this.showForm = false;
      });
    }
  }
}
