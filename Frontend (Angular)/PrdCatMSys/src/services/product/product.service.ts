import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, of } from 'rxjs';
import { Product } from '../../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7112/products'; 

  constructor(private http: HttpClient) {}

  getProducts(): Observable<{ entity: Product[]; isSuccess: boolean; msg: string }> {
    return this.http.get<{ entity: Product[]; isSuccess: boolean; msg: string }>(`${this.apiUrl}`).pipe(
      catchError(error => {
        console.error('Error fetching products:', error);
        return of({ entity: [], isSuccess: false, msg: 'Failed to fetch products' });
      })
    );
  }
  
  getProductById(id: string): Observable<{ entity: Product; isSuccess: boolean; msg: string }> {
    return this.http.get<{ entity: Product; isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`).pipe(
      catchError(error => {
        console.error(`Error fetching product with ID ${id}:`, error);
        return of({ entity: null as any, isSuccess: false, msg: 'Failed to fetch product' });
      })
    );
  }
  

  createProduct(product: Product): Observable<{ entity: Product; isSuccess: boolean; msg: string }> {
    return this.http.post<{ entity: Product; isSuccess: boolean; msg: string }>(`${this.apiUrl}`, product).pipe(
      catchError(error => {
        console.error('Error creating product:', error);
        return of({ entity: null as any, isSuccess: false, msg: 'Failed to create product' });
      })
    );
  }
  

  updateProduct(id: string, product: Product): Observable<{ entity: Product; isSuccess: boolean; msg: string }> {
    return this.http.put<{ entity: Product; isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`, product).pipe(
      catchError(error => {
        console.error(`Error updating product with ID ${id}:`, error);
        return of({ entity: null as any, isSuccess: false, msg: 'Failed to update product' });
      })
    );
  }
  

  deleteProduct(id: string): Observable<{ isSuccess: boolean; msg: string }> {
    return this.http.delete<{ isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`).pipe(
      catchError(error => {
        console.error(`Error deleting product with ID ${id}:`, error);
        return of({ isSuccess: false, msg: 'Failed to delete product' });
      })
    );
  }
  

  deleteSelectedProducts(ids: number[]): Observable<{ isSuccess: boolean; msg: string }> {
    return this.http.delete<{ isSuccess: boolean; msg: string }>(`${this.apiUrl}/batch`, { body: ids }).pipe(
      catchError(error => {
        console.error('Error deleting selected products:', error);
        return of({ isSuccess: false, msg: 'Failed to delete selected products' });
      })
    );
  }
  
  
}
