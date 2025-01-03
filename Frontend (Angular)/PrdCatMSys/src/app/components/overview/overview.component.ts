import { Component } from '@angular/core';
import { StatiticsService } from '../../../services/statictics/statitics.service';
import { ProductService } from '../../../services/product/product.service';
import { Product } from '../../../models/product';

@Component({
  selector: 'app-overview',
  imports: [],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})
export class OverviewComponent {
  totalProducts: number = 0;
  totalCategories: number = 0;
  lowStockCount: number = 0;
  products: Product[] = [];
  constructor(private statisticsService: StatiticsService ,private productService: ProductService ) { }

  ngOnInit(): void {
    this.fetchCounts();
  }

  fetchCounts(): void {
    this.statisticsService.getTotalProductsCount().subscribe(
      (count) => {
        this.totalProducts = count;
      },
      (error) => {
        console.error('Error fetching product count:', error);
      }
    );

    this.statisticsService.getTotalCategoriesCount().subscribe(
      (count) => {
        this.totalCategories = count;
      },
      (error) => {
        console.error('Error fetching category count:', error);
      }
    );
  }

  getProductsAndCountLowStock(): void {
    this.productService.getProducts().subscribe(response => {
      if (response.isSuccess) {
        this.products = response.entity; 
        this.lowStockCount = this.products.filter(product => product.stockQuantity < 10).length;
        console.log(this.lowStockCount);
        
      } else {
        console.error('Error fetching products:', response.msg);
      }
    });
  }
}
