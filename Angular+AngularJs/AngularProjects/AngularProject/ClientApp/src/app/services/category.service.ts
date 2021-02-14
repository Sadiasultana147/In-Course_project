import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { dataUrl } from '../shared/common/constants';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private http: HttpClient
  ) { }
  getWithBook(): Observable<Category[]> {
    return this.http.get<Category[]>(`${dataUrl}/CategoryData/CategoriesWithBook`);
  }
  getWithBookById(id: number): Observable<Category> {
    return this.http.get<Category>(`${dataUrl}/CategoryData/CategoriesWithBookById/${id}`);
  }
  insert(t: Category): Observable<Category> {
    // console.log(t);
    return this.http.post<Category>(`${dataUrl}/CategoryData/InsertCategoriesWithBook`, t);
  }
  update(t: Category): Observable<Category> {
    return this.http.put<Category>(`${dataUrl}/CategoryData/UpdateCategoriesWithBook/${t.CategoryId}`, t);
  }
  delete(id: number): Observable<Category> {
    return this.http.delete<Category>(`${dataUrl}/CategoryData/DeleteCategory/${id}`);
  }
  deleteBook(id: number): Observable<Book> {
    return this.http.delete<Book>(`${dataUrl}/BookData/DeleteBook/${id}`);
  }
}
