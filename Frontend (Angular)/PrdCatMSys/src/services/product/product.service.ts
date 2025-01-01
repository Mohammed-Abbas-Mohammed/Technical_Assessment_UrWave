import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7112/api/products'; 

  constructor(private http: HttpClient) {}

  getProducts(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  getProductById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  createProduct(product: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, product);
  }

  updateProduct(id: string, product: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  deleteSelectedProducts(ids: number[]): Observable<any> {
    return this.http.delete<any>('/products/batch', { body: ids });
  }
  
}
