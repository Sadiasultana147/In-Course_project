import { Book } from './book';
import { MatTableDataSource } from '@angular/material/table';

export class Category {
  constructor(
    public CategoryId?: number,
    public CategoryName?: string,
    
    public Books?: Book[] | MatTableDataSource<Book>
  ) { }
}
export interface CategoryDataSource {
  CategoryId?: number;
  CategoryName?: string;
 
  Books?: MatTableDataSource<Book>;
}
