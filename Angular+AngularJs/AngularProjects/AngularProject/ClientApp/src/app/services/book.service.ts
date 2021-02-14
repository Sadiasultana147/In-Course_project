import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { dataUrl } from '../shared/common/constants';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(
    private http: HttpClient
  ) { }
  get(): Observable<Book[]> {
    return this.http.get<Book[]>(`${dataUrl}/BookData/GetBooks`);
  }
  getById(id: number): Observable<Book> {
    return this.http.get<Book>(`${dataUrl}/BookData/GetBookById/${id}`);
  }
  getCategoryOptions(): Observable<Category[]> {
    return this.http.get<Category[]>(`${dataUrl}/BookData/GetCategoriesForDropDown`);
  }
  insert(b: Book): Observable<Book> {
    return this.http.post<Book>(`${dataUrl}/BookData/InsertBook`, b);
  }
  update(b: Book): Observable<Book> {
    return this.http.put<Book>(`${dataUrl}/BookData/UpdateBook/${b.BookId}`, b);
  }
  delete(id: number): Observable<Book> {
    return this.http.delete<Book>(`${dataUrl}/BookData/DeleteBook/${id}`);
  }
}
