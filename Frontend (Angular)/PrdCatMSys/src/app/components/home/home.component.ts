import { Component } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CategoryService } from '../../../services/category/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  products: any[] = [];
  filteredProducts: any[] = [];
  categories: any[] = [];
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
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
      this.filteredProducts = data;
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  filterByCategory(categoryId: string) {
    this.filteredProducts = this.products.filter((p) => p.categoryId === categoryId);
  }

  filterByStatus() {
    this.filteredProducts = this.products.filter((p) => p.status === this.selectedStatus);
  }
}
