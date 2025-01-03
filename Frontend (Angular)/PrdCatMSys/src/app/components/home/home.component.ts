import { Component } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CategoryService } from '../../../services/category/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../../models/product';
import { Category } from '../../../models/category';
import { EntityStatus } from '../../../models/Status';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  categories: Category[] = [];
  statuses: string[] = ['Available', 'OutOfStock', 'Discontinued'];
  priceRange: number = 500;
  selectedStatus: string = '';

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts() {
    this.productService.getProducts().subscribe((response) => {
  if(response.isSuccess){
      this.products = response.entity;
      this.filteredProducts = response.entity;
    }
  else{
    console.error(response.msg);
  }
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(response => {
      if (response.isSuccess) {
        this.categories = response.entity; // Populate categories
      } else {
        console.error(response.msg); // Display error message
      }
    });
  }

  filterByCategory(categoryId: string) {
    this.filteredProducts = this.products.filter((p) => p.categoryId === categoryId);
  }

  filterByStatus() {
    this.filteredProducts = this.products.filter((p) => p.status === EntityStatus.Active); //change it
  }
}
