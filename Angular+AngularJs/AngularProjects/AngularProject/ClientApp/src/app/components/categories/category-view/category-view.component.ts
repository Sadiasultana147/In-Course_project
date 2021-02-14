import { Component, OnInit, ViewChild, QueryList, ViewChildren, ChangeDetectorRef } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { throwError } from 'rxjs';
import { Book } from 'src/app/models/book';
import { NotifyService } from 'src/app/services/notify.service';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from '../../common/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class CategoryViewComponent implements OnInit {
  @ViewChild('outerSort', { static: true }) sort: MatSort;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort>;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<Book>>;
  @ViewChild(MatPaginator, { static: false }) paginator;
  //set paginator(value: MatPaginator) {
  //  this.dataSource.paginator = value;
  //}
  //dataSource: MatTableDataSource<User>; //
  dataSource: MatTableDataSource<Category>;
  //usersData: User[] = []; //
  categoriesData: Category[] = [];
  columnsToDisplay = ['select', 'CategoryName', 'actions']; //['select', 'name', 'email', 'phone', 'actions'];
  innerDisplayedColumns = ['BookName', 'Author', 'Price', 'actions']; //['street', 'zipCode', 'city', 'actions']; //
  expandedElement: Category | null; //User | null; //
  constructor(
    private categoryService: CategoryService,
    private notifyService: NotifyService,
    private deleteDialog: MatDialog,
    private cd: ChangeDetectorRef
  ) { }
  initTable(data:Category[]) {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  confirmDelete(item: Category) {
    let dialogRef = this.deleteDialog.open(DeleteDialogComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.categoryService.delete(item.CategoryId)
            .subscribe(x => {
              this.dataSource.data = this.dataSource.data.filter(data => data.CategoryId != item.CategoryId);
              this.notifyService.message("Data deleted.", ["DISMISS"])
            });
        }
        else { console.log("let it go"); }
      }
    );
  }
  confirmDeleteBook(item: Book) {
    console.log(this.expandedElement);
    console.log(item);
    let dialogRef = this.deleteDialog.open(DeleteDialogComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.categoryService.deleteBook(item.BookId)
            .subscribe(x => {
              let ds = this.expandedElement.Books as MatTableDataSource<Book>;
              ds.data = ds.data.filter(data => data.BookId != item.BookId);
              this.notifyService.message("Data deleted.", ["DISMISS"])
            });
        }
        else { console.log("let it go"); }
      }
    );
  }
  ngOnInit() {

   
    this.categoryService.getWithBook()
      .subscribe(x => {
        this.categoriesData = x;
        //USERS.forEach(user => {
        //  if (user.addresses && Array.isArray(user.addresses) && user.addresses.length) {
        //    this.usersData = [...this.usersData, { ...user, addresses: new MatTableDataSource(user.addresses) }];
        //  } else {
        //    this.usersData = [...this.usersData, user];
        //  }
        //});
        this.categoriesData.forEach(category => {
          if ((category.Books as Book[]).length == 0) category.Books = null;
          if (category.Books && Array.isArray(category.Books) && category.Books.length) {
            category.Books = new MatTableDataSource(category.Books);
          } 
        });
        console.log(this.categoriesData);
        this.initTable(this.categoriesData);
       
        

      }, err => {
        console.log(err);
        return throwError(err);
      })
    
  }

  toggleRow(element: Category) {
    element.Books && (element.Books as MatTableDataSource<Book>).data.length ? (this.expandedElement = this.expandedElement === element ? null : element) : null;
    this.cd.detectChanges();
    this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Book>).sort = this.innerSort.toArray()[index]);
  }

  applyFilter(filterValue: string) {
    //this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Address>).filter = filterValue.trim().toLowerCase());
    this.dataSource.filter = filterValue.trim().toLowerCase()
  }

}
