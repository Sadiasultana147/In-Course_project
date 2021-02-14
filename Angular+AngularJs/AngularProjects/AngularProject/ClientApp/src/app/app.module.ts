import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppNavComponent } from './app-nav/app-nav.component';
import { LayoutModule } from '@angular/cdk/layout';

import { HomeComponent } from './components/home/home.component';
import { MatIncludeModule } from './shared/common/mat-include/mat-include.module';
import { HttpClientModule } from '@angular/common/http';
import { CategoryService } from './services/category.service';
import { BookService } from './services/book.service';
import { CategoryViewComponent } from './components/categories/category-view/category-view.component';
//import { NgMaterialMultilevelMenuModule } from 'ng-material-multilevel-menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BookViewComponent } from './components/books/book-view/book-view.component';
import { BookCreateComponent } from './components/books/book-create/book-create.component';
import { BookEditComponent } from './components/books/book-edit/book-edit.component';
import { CategoryCreateComponent } from './components/categories/category-create/category-create.component';
import { CategoryEditComponent } from './components/categories/category-edit/category-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { NotifyService } from './services/notify.service';
import { DeleteDialogComponent } from './components/common/delete-dialog/delete-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    AppNavComponent,
    HomeComponent,
    CategoryViewComponent,
    CategoryCreateComponent,
    CategoryEditComponent,
    BookViewComponent,
    BookCreateComponent,
    DeleteDialogComponent,
    BookEditComponent
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    LayoutModule,
    MatIncludeModule,
    HttpClientModule,
    //NgMaterialMultilevelMenuModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    
  ],
  entryComponents: [DeleteDialogComponent],
  providers: [CategoryService, BookService, NotifyService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
