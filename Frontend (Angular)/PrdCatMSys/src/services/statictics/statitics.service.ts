import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatiticsService {
  private apiUrl = 'https://localhost:7112/api/products'; 
  constructor(private http: HttpClient) { }

  // Fetch all products and count them
  getTotalProductsCount(): Observable<number> {
    return this.http.get<any[]>(`${this.apiUrl}/products`) // Get all products
      .pipe(
        map(products => products.length) // Calculate the length of the array (count)
      );
  }

  // Fetch all categories and count them
  getTotalCategoriesCount(): Observable<number> {
    return this.http.get<any[]>(`${this.apiUrl}/categories`) // Get all categories
      .pipe(
        map(categories => categories.length) // Calculate the length of the array (count)
      );
  }
}
