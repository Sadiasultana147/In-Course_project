import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { NotifyService } from 'src/app/services/notify.service';
import { Category } from 'src/app/models/category';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { Book } from 'src/app/models/book';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ]
})
export class CategoryCreateComponent implements OnInit {
  category: Category = new Category();
  categoryForm: FormGroup = new FormGroup({
    CategoryName: new FormControl('', Validators.required),
    
    Books: new FormArray([])
  });
  removeItem(index: number) {
    (this.category.Books as Book[]).splice(index, 1);
  }
  onSubmit() {
    console.log(this.isDisabled);
    if (this.categoryForm.controls.CategoryName.invalid 
      
    )
    {
      this.notifyService.message("Errors in form", 'DISMISS')
      return;
    }
    if ((this.category.Books as Book[]).length == 0)
    {
      this.notifyService.message("No book added.", 'DISMISS')
      return;
    }
    this.category.CategoryName = this.categoryForm.controls.CategoryName.value;

    this.category.CategoryId = 0;
    console.log(this.category);

    this.categoryService.insert(this.category)
      .subscribe(x => {
        this.notifyService.message("Data Saved successfully", 'DISMISS');
        this.categoryForm.reset({});
        this.categoryForm.markAsUntouched();
        this.categoryForm.markAsPristine();
        this.category = new Category();
        this.category.Books = [];
      }, err => {
        this.notifyService.message("Could not insert data.", 'DISMISS');
        return throwError(err);
      })
  }
  addBookForm() {
    (this.categoryForm.get('Books') as FormArray).push(
      new FormGroup({
        BookName: new FormControl('', Validators.required),
        Author: new FormControl('', Validators.required),
        Price: new FormControl('', Validators.required)
      }));
  }
  addBookToCategory() {
    console.log(this.BookArray.controls[0].value);
    let book = new Book();
    Object.assign(book, this.BookArray.controls[0].value);
    (this.category.Books as Book[]).push(book);
    this.BookArray.controls[0].reset({});
    this.BookArray.controls[0].markAsPristine();
    this.BookArray.controls[0].markAsUntouched();
  }
  get BookArray() {
    return this.categoryForm.get("Books") as FormArray;
  }
  get bookLength() {
    return (this.category.Books ? (this.category.Books as Book[]).length : 0)
  }
  get isDisabled() {
    return this.categoryForm.controls.CategoryName.invalid || (this.category.Books as Book[]).length == 0;
      
  }
  constructor(
    private categoryService: CategoryService,
    private notifyService: NotifyService
  ) { }

  ngOnInit(): void {
    this.category.Books = [];
    this.addBookForm();
  }

}
