
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private baseUrl = 'https://localhost:7112/api/categories'; 

  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/categories`);
  }

  getCategoryById(id: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  createCategory(category: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, category);
  }

  updateCategory(id: string, category: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, category);
  }

  deleteCategory(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  deleteSelectedCategories(ids: number[]): Observable<any> {
    return this.http.delete<any>('/categories/batch', { body: ids });
  }
  
}

