import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DocumentsListComponent } from './feature/documents-list/documents-list.component';
import { DocumentsRoutingModule } from './documents-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationComponent } from './ui/pagination/pagination.component';
import { PaginationBarComponent } from './ui/pagination-footer-bar/pagination-bar.component';

@NgModule({
  declarations: [DocumentsListComponent],
  imports: [CommonModule, DocumentsRoutingModule, FormsModule, ReactiveFormsModule, PaginationComponent, PaginationBarComponent],
})
export class DocumentsListModule {}
