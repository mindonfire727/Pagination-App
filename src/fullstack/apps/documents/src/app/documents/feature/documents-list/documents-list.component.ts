import { Component, OnInit, OnDestroy, ChangeDetectionStrategy, inject } from '@angular/core';
import { Subject, debounceTime, takeUntil } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DestroyableDirective } from '../../ui/destroyable.directive';
import { DocumentDto, GetDocumentsRequest } from '../../models/document-models';
import { DocumentsService } from '../../data-access/documents.service';

@Component({
  selector: 'fullstack-documents',
  templateUrl: './documents-list.component.html',
  styleUrls: ['./documents-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocumentsListComponent extends DestroyableDirective implements OnInit, OnDestroy {
  public myForm: FormGroup;
  public errorMessage = '';
  public documents: DocumentDto[]=[];
  public isSuccess = new Subject<boolean>();
  private formBuilder = inject(FormBuilder);
  private documentService = inject(DocumentsService);
  private request: GetDocumentsRequest = new GetDocumentsRequest();
  public numberOfPages = 0;

  constructor() {
    super();
    this.myForm = this.formBuilder.group({
      searchQuery: ''
    });
  }

  ngOnInit() {
    this.myForm.get('searchQuery')?.valueChanges
      .pipe(debounceTime(700),
      takeUntil(this.destroy$))
      .subscribe((value) => {
        this.request.searchQuery = value;
        this.request.currentPage = 1;
        this.getDocuments();
      });
    this.getDocuments();
  }

  onPageSizeChanged(value: number){
      this.request.take.amount = value;
      this.getDocuments();
  }

  onChangedPage(selectedPage: number){
    this.request.currentPage = selectedPage;
    this.getDocuments();
  }

  getDocuments() {
    this.documentService.all(this.request).subscribe({
      next: (x) => {
      this.documents = x.paginationList.items;
      this.numberOfPages = x.paginationList.totalPages;
      this.isSuccess.next(true);
    },
    error: (error) => {
      this.errorMessage = error;
      this.isSuccess.next(false);
      this.numberOfPages = 0;
    }});
  }

  override ngOnDestroy() {
    super.ngOnDestroy();
  }
}

