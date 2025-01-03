
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, of } from 'rxjs';
import { Category } from '../../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'https://localhost:7112/categories'; 

  constructor(private http: HttpClient) {}
 // Fetch all categories
 getCategories(): Observable<{ entity: Category[]; isSuccess: boolean; msg: string }> {
  return this.http.get<{ entity: Category[]; isSuccess: boolean; msg: string }>(this.apiUrl).pipe(
    catchError(error => {
      console.error('Error fetching categories:', error);
      return of({ entity: [], isSuccess: false, msg: 'Failed to fetch categories' });
    })
  );
}

// Fetch category by ID
getCategoryById(id: string): Observable<{ entity: Category; isSuccess: boolean; msg: string }> {
  return this.http.get<{ entity: Category; isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`).pipe(
    catchError(error => {
      console.error(`Error fetching category with ID ${id}:`, error);
      return of({ entity: null as any, isSuccess: false, msg: 'Failed to fetch category' });
    })
  );
}

// Create a new category
createCategory(category: Category): Observable<{ entity: Category; isSuccess: boolean; msg: string }> {
  return this.http.post<{ entity: Category; isSuccess: boolean; msg: string }>(this.apiUrl, category).pipe(
    catchError(error => {
      console.error('Error creating category:', error);
      return of({ entity: null as any, isSuccess: false, msg: 'Failed to create category' });
    })
  );
}

// Update an existing category
updateCategory(id: string, category: Category): Observable<{ entity: Category; isSuccess: boolean; msg: string }> {
  return this.http.put<{ entity: Category; isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`, category).pipe(
    catchError(error => {
      console.error(`Error updating category with ID ${id}:`, error);
      return of({ entity: null as any, isSuccess: false, msg: 'Failed to update category' });
    })
  );
}

// Delete a category
deleteCategory(id: string): Observable<{ isSuccess: boolean; msg: string }> {
  return this.http.delete<{ isSuccess: boolean; msg: string }>(`${this.apiUrl}/${id}`).pipe(
    catchError(error => {
      console.error(`Error deleting category with ID ${id}:`, error);
      return of({ isSuccess: false, msg: 'Failed to delete category' });
    })
  );
}

// Delete selected categories in batch
deleteSelectedCategories(ids: number[]): Observable<{ isSuccess: boolean; msg: string }> {
  return this.http.delete<{ isSuccess: boolean; msg: string }>(`${this.apiUrl}/batch`, { body: ids }).pipe(
    catchError(error => {
      console.error('Error deleting selected categories:', error);
      return of({ isSuccess: false, msg: 'Failed to delete selected categories' });
    })
  );
}
  
}

