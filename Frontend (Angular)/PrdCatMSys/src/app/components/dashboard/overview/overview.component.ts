import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-overview',
  imports: [
    CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    TableModule,
    ButtonModule
  ],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})
export class OverviewComponent implements OnInit {
  products: any[] = [];

  constructor() {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.products = [
      {
        id: 1,
        name: 'Laptop',
        category: 'Electronics',
        price: 1200,
        stock: 10,
      },
      {
        id: 2,
        name: 'Smartphone',
        category: 'Electronics',
        price: 800,
        stock: 15,
      },
      {
        id: 3,
        name: 'Headphones',
        category: 'Accessories',
        price: 150,
        stock: 20,
      },
    ];
  }
}
