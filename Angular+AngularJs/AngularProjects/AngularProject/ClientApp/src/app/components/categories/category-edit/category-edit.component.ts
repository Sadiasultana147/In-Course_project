import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { NotifyService } from 'src/app/services/notify.service';
import { Category } from 'src/app/models/category';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { Book } from 'src/app/models/book';
import { throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ]
})
export class CategoryEditComponent implements OnInit {
  category: Category = null;
  categoryForm: FormGroup = new FormGroup({
    CategoryName: new FormControl('', Validators.required),
   
    Books: new FormArray([]),
    NewBooks: new FormArray([])
  });
  removeItem(index: number) {
    (this.category.Books as Book[]).splice(index, 1);
  }
  onSubmit() {
    if (this.categoryForm.controls.CategoryName.invalid ||

      (this.category.Books as Book[]).length == 0
    ) return;
    this.category.CategoryName = this.categoryForm.controls.CategoryName.value;
    let i = 0;
    for (let x of this.BookArray.controls) {
      console.log();
      (this.category.Books as Book[])[i].BookName = x.get("BookName").value;
      (this.category.Books as Book[])[i].Author = x.get("Author").value;
      (this.category.Books as Book[])[i].Price = x.get("Price").value;
      i++;
    }
    

    console.log(this.category);

    this.categoryService.update(this.category)
      .subscribe(x => {
        this.notifyService.message("Data Saved successfully", 'DISMISS');
        
        this.categoryForm.markAsUntouched();
        this.categoryForm.markAsPristine();
        this.category = new Category();
       
        
      }, err => {
        this.notifyService.message("Could not insert data.", 'DISMISS');
        return throwError(err);
      })
  }
  addBookForm() {
    (this.categoryForm.get('NewBooks') as FormArray).push(
      new FormGroup({
        BookName: new FormControl('', Validators.required),
        Author: new FormControl('', Validators.required),
        Price: new FormControl('', Validators.required)
      }));
  }
  addBookToCategory() {
    console.log(this.NewBookArray.controls[0].value);
    let book = new Book();
    Object.assign(book, this.NewBookArray.controls[0].value);
    (this.category.Books as Book[]).push(book);
    (this.categoryForm.get('Books') as FormArray).push(
      new FormGroup({
        BookName: new FormControl(book.BookName, Validators.required),
        Author: new FormControl(book.Author, Validators.required),
        Price: new FormControl(book.Price, Validators.required)
      }));
    this.NewBookArray.controls[0].reset({});
    this.NewBookArray.controls[0].markAsPristine();
    this.NewBookArray.controls[0].markAsUntouched();
  }
  get BookArray() {
    return this.categoryForm.get("Books") as FormArray;
  }
  get NewBookArray() {
    return this.categoryForm.get("NewBooks") as FormArray;
  }
  initForm() {
    this.categoryForm.setValue({
      CategoryName: this.category.CategoryName,
      
      Books: [],
      NewBooks: []
    });
    for (let x of this.category.Books as Book[]) {
      (this.categoryForm.get('Books') as FormArray).push(
        new FormGroup({
          BookName: new FormControl(x.BookName, Validators.required),
          Author: new FormControl(x.Author, Validators.required),
          Price: new FormControl(x.Price, Validators.required)
        }));
    }
    this.addBookForm();
  }
  removeBookItem(index: number) {
    this.BookArray.removeAt(index);
  }
  constructor(
    private categoryService: CategoryService,
    private notifyService: NotifyService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params.id;
    console.log(id);
    this.categoryService.getWithBookById(id)
      .subscribe(x => {
        console.log(x);
        this.category = x;
        this.initForm();
      }, err => {
        this.notifyService.message("Failed to fetch category data.", 'DISMISS');
        return throwError(err);
      })
   
  }
 

}
