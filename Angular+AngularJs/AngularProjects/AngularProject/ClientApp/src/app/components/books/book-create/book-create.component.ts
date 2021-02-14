import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { Book } from 'src/app/models/book';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category';

@Component({
  selector: 'app-book-create',
  templateUrl: './book-create.component.html',
  styleUrls: ['./book-create.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ]
})
export class BookCreateComponent implements OnInit {
  book: Book = new Book();
  categories: Category[] = [];
  bookForm: FormGroup = new FormGroup({
    BookName: new FormControl('', Validators.required),
    Author: new FormControl('', Validators.required),
    Price: new FormControl('', Validators.required),
    CategoryId: new FormControl('', Validators.required)
  })
  constructor(
    private bookService:BookService
  ) { }
  get f() {
    return this.bookForm.controls;
  }
  onSubmit() {
    if (this.bookForm.invalid) return;
    Object.assign(this.book, this.bookForm.value);
    this.bookService.insert(this.book)
      .subscribe(x => {
        this.bookForm.reset({});
        this.bookForm.markAsUntouched();
        this.bookForm.markAsPristine();
      }, err => {
        console.log(err);

      })
  }
  ngOnInit(): void {
    this.bookService.getCategoryOptions()
      .subscribe(x => {
        this.categories = x;
      }, err => {

      })
  }

}
