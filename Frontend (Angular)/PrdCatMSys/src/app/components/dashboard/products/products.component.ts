import { Component } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-products',
  imports: [CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    TableModule,
    ButtonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  products = [
    { name: 'Product 1', price: 100 },
    { name: 'Product 2', price: 200 },
  ];

  addProduct() {
    console.log('Add Product');
  }

  editProduct(product: any) {
    console.log('Edit Product:', product);
  }

  deleteProduct(product: any) {
    console.log('Delete Product:', product);
  }
}
