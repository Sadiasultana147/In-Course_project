import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { Book } from 'src/app/models/book';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent implements OnInit {

  book: Book = null;
  categories: Category[] = [];
  bookForm: FormGroup = new FormGroup({
    BookName: new FormControl('', Validators.required),
    Author: new FormControl('', Validators.required),
    Price: new FormControl('', Validators.required),
    CategoryId: new FormControl('', Validators.required)
  })
  constructor(
    private bookService: BookService,
    private activatedRoute: ActivatedRoute
  ) { }
  get f() {
    return this.bookForm.controls;
  }
  onSubmit() {
    if (this.bookForm.invalid) return;
    Object.assign(this.book, this.bookForm.value);
    console.log(this.book);
    this.bookService.update(this.book)
      .subscribe(x => {
       
        this.bookForm.markAsUntouched();
        this.bookForm.markAsPristine();
      }, err => {
        console.log(err);

      })
  }
  initForm() {
    this.bookForm.setValue({
      BookName: this.book.BookName,
      Author: this.book.Author,
      Price: this.book.Price,
      CategoryId: this.book.CategoryId
    })
  }
  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params.id;
    this.bookService.getCategoryOptions()
      .subscribe(x => {
        this.categories = x;
       
      }, err => {

      });
    this.bookService.getById(id)
      .subscribe(x => {
        this.book = x;
        this.initForm();
      }, err => {

      })
  }

}
