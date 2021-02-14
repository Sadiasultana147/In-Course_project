import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CategoryViewComponent } from './components/categories/category-view/category-view.component';
import { BookViewComponent } from './components/books/book-view/book-view.component';
import { BookCreateComponent } from './components/books/book-create/book-create.component';
import { BookEditComponent } from './components/books/book-edit/book-edit.component';
import { CategoryCreateComponent } from './components/categories/category-create/category-create.component';
import { CategoryEditComponent } from './components/categories/category-edit/category-edit.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'categories', component: CategoryViewComponent },
  { path: 'books', component: BookViewComponent },
  { path: 'createBook', component: BookCreateComponent },
  { path: 'editBook/:id', component: BookEditComponent },
  { path: 'createCategory', component: CategoryCreateComponent },
  { path: 'editCategory/:id', component: CategoryEditComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
