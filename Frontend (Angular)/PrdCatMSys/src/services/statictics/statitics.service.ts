import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Category } from '../../models/category';
import { Product } from '../../models/product';

@Injectable({
  providedIn: 'root'
})
export class StatiticsService {
  private apiUrl = 'https://localhost:7112'; 
  constructor(private http: HttpClient) { }

  getTotalProductsCount(): Observable<number> {
    return this.http.get<{ entity: Product[]; isSuccess: boolean; msg: string }>(`${this.apiUrl}/products`).pipe(
      map(response => {
        if (response.isSuccess) {
          console.log('Products Entity:', response.entity); // Debug response
          return response.entity.length; // Return the count of products
        } else {
          console.warn('Failed to fetch products:', response.msg); // Log the error message
          return 0; // Return 0 if fetching fails
        }
      }),
      catchError(error => {
        console.error('Error fetching products:', error);
        return of(0); // Handle errors by returning 0
      })
    );
  }
  
  getTotalCategoriesCount(): Observable<number> {
    return this.http.get<{ entity: Category[]; isSuccess: boolean; msg: string }>(`${this.apiUrl}/categories`).pipe(
      map(response => {
        if (response.isSuccess) {
          console.log('Categories Entity:', response.entity); // Debug response
          return response.entity.length; // Return the count of categories
        } else {
          console.warn('Failed to fetch categories:', response.msg); // Log the error message
          return 0; // Return 0 if fetching fails
        }
      }),
      catchError(error => {
        console.error('Error fetching categories:', error);
        return of(0); // Handle errors by returning 0
      })
    );
  }
  
}
