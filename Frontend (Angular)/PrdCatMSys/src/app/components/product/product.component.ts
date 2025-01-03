import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: any[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((response) => {
      if(response.isSuccess){
          this.products = response.entity;
        }
      else{
        console.error(response.msg);
      }
        });
  }

  // Edit product (stub for now)
  editProduct(productId: string): void {
    console.log('Edit product with ID:', productId);
  }

  // Delete product
  deleteProduct(productId: string): void {
    this.productService.deleteProduct(productId).subscribe({
      next: (response: any) => {
        if (response.isSuccess) {
          this.products = this.products.filter(product => product.id !== productId);
        } else {
          console.error(response.msg);
        }
      },
      error: (error) => {
        console.error('Error deleting product:', error);
      }
    });
  }
}
